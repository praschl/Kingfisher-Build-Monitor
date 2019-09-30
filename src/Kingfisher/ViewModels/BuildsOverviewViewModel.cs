using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using Kingfisher.Provider;
using Kingfisher.Provider.Configuration;
using Kingfisher.ViewModels.Mappers;

namespace Kingfisher.ViewModels
{
    public class BuildsOverviewViewModel : ViewModelBase, IExceptionViewModel
    {
        private readonly object _buildLock = new object();

        private readonly DispatcherTimer _buildRefreshTimer = new DispatcherTimer();

        private readonly IBuildsMapper _buildsMapper;
        private readonly IBuildsProvider _buildsProvider;
        private readonly IConfigManager _configManager;
        private readonly IProjectMapper _projectMapper;
        private readonly object _projectsLock = new object();
        private readonly DispatcherTimer _refreshTimePropertiesTimer = new DispatcherTimer();
        private readonly DevOpsServerConfig _serverConfig;

        public BuildsOverviewViewModel(IBuildsProvider buildsProvider, IProjectMapper projectMapper, IBuildsMapper buildsMapper, IConfigManager configManager)
        {
            _buildsProvider = buildsProvider;
            _projectMapper = projectMapper;
            _buildsMapper = buildsMapper;
            _configManager = configManager;

            _serverConfig = _configManager.Get<DevOpsServerConfig>();

            BindingOperations.EnableCollectionSynchronization(Projects, _projectsLock);
            BindingOperations.EnableCollectionSynchronization(Builds, _buildLock);

            BuildsCollectionView = new ListCollectionView(Builds)
            {
                Filter = FilterBuild,
                SortDescriptions =
                {
                    new SortDescription(nameof(BuildViewModel.ChangedAt), ListSortDirection.Descending)
                }
            };

            _buildRefreshTimer.Tick += BuildRefreshTimerTickHandler;

            _refreshTimePropertiesTimer.Interval = TimeSpan.FromSeconds(1);
            _refreshTimePropertiesTimer.Tick += RefreshTimePropertiesTickHandler;
            _refreshTimePropertiesTimer.Start();
        }

        public string SearchText { get; set; } = string.Empty;

        public ObservableCollection<ProjectViewModel> Projects { get; set; } = new ObservableCollection<ProjectViewModel>();

        public ObservableCollection<BuildViewModel> Builds { get; set; } = new ObservableCollection<BuildViewModel>();
        public ICollectionView BuildsCollectionView { get; }

        public bool IsRefreshingBuilds { get; set; }

        public bool DisplaySpinner => IsRefreshingBuilds && Builds.Count == 0;

        public Exception Exception { get; set; }

        private bool FilterBuild(object obj)
        {
            var search = SearchText;
            if (string.IsNullOrWhiteSpace(search))
                return true;

            if (!(obj is BuildViewModel build))
                return false;

            var result = build.RequestedFor.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)
                         ||
                         build.RequestedForShort.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)
                         ||
                         build.RequestedBy.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)
                         ||
                         build.RequestedByShort.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)
                         ||
                         build.Project.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)
                         ||
                         build.Definition.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)
                ;

            return result;
        }

        private void OnSearchTextChanged()
        {
            BuildsCollectionView.Refresh();
        }

        internal void RestartTimer(bool refresh = false)
        {
            _buildRefreshTimer.Stop();
            if (refresh)
            {
                Builds.Clear();
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DisplaySpinner)));
            }

            if (string.IsNullOrWhiteSpace(_configManager.Get<DevOpsServerConfig>().Url))
                return;

            _buildRefreshTimer.Interval = _serverConfig.BuildRefreshTime;
            _buildRefreshTimer.Start();

            // In general, not awaiting an async Method (which returns a Task) will cause any Exception in the method being lost, going nowhere.
            // Awaiting the Task, will throw the exception on the current thread.
            // async void Methods cannot be awaited, BUT an Exception will hit the application completely unhandled, usually causing it to crash.
            // Thus, async void Methods (usually only event handlers) must handle all exceptions themselves.

            // Here, we want to call an async method which does return a Task. However, we do not want to await it.
            // async () => await X() is effectively creating an async void method inline, so any Exception which leaves, would at least crash the application, so we know
            // Since RefreshAllAsync handles it's exceptions, we can do that without crashing the application.

            Task.Run(async () => await RefreshAllAsync().ConfigureAwait(false));
        }

        // Event handler may be async void
        private async void BuildRefreshTimerTickHandler(object sender, EventArgs e)
        {
            await RefreshAllAsync().ConfigureAwait(false);
        }

        private void RefreshTimePropertiesTickHandler(object sender, EventArgs e)
        {
            // for loop does not cause "Collection has been modified" problems,
            // when Builds are being refreshed while this timer runs.

            // DO NOT CHANGE TO FOREACH!
            for (var i=0;i<Builds.Count; i++)
                Builds[i].RefreshTimeProperties();
        }

        // this catches all exceptions which could occur - no exception will ever leave from here
        private async Task RefreshAllAsync()
        {
            if (IsRefreshingBuilds)
                return;

            try
            {
                IsRefreshingBuilds = true;
                if (string.IsNullOrEmpty(_configManager.Get<DevOpsServerConfig>().Url))
                    return;

                await RefreshProjectsAsync().ConfigureAwait(false);
                await RefreshBuildsAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            finally
            {
                IsRefreshingBuilds = false;
            }
        }

        private async Task RefreshProjectsAsync()
        {
            var projects = await _buildsProvider.GetProjectsAsync();
            _projectMapper.Map(projects, Projects);
        }

        private async Task RefreshBuildsAsync()
        {
            var projects = Projects.Select(p => p.Name).ToArray();

            var since = Builds.Count == 0
                    ? DateTimeOffset.Now -_serverConfig.AgeOfBuilds
                    // now - refreshtime - 5 secs to allow for some overlapping
                    : DateTimeOffset.Now - _serverConfig.BuildRefreshTime.Add(TimeSpan.FromSeconds(5))
                ;

            var finishedBuilds = _buildsProvider.GetFinishedBuildsAsync(projects, since);
            var openBuilds = _buildsProvider.GetOpenBuildsAsync(projects);

            _buildsMapper.Map(await openBuilds, Builds);
            _buildsMapper.Map(await finishedBuilds, Builds);
        }

        public class Designer : BuildsOverviewViewModel
        {
            public Designer()
                : base(null, null, null, new JsonConfigManager())
            {
                SearchText = "dev";
                Exception = new NotImplementedException();
                Builds = new ObservableCollection<BuildViewModel>
                {
                    BuildViewModel.DesignTimeModel,
                    new BuildViewModel
                    {
                        Id = 2,
                        Project = "My Project 2",
                        Definition = "My Definition 2",
                        Status = BuildState.failed,
                        QueuedDateTime = DateTime.Now.AddDays(-3)
                    }
                };
            }

            public static Designer Instance => new Designer();
        }
    }
}