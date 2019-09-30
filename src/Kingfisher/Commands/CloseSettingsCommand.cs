using System;
using System.Windows.Input;
using Kingfisher.Provider.Configuration;
using Kingfisher.Provider.Utils;
using Kingfisher.ViewModels;

namespace Kingfisher.Commands
{
    public class CloseSettingsCommand : ICommand
    {
        private readonly BuildsOverviewViewModel _buildsOverviewViewModel;
        private readonly IConfigManager _configManager;
        private readonly ConfigurationViewModel _configurationViewModel;
        private readonly PageViewModel _pageViewModel;

        public CloseSettingsCommand(PageViewModel pageViewModel, ConfigurationViewModel configurationViewModel, BuildsOverviewViewModel buildsOverviewViewModel, IConfigManager configManager)
        {
            _pageViewModel = pageViewModel;
            _configurationViewModel = configurationViewModel;
            _buildsOverviewViewModel = buildsOverviewViewModel;
            _configManager = configManager;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if ((string) parameter == "OK")
            {
                var devOpsServerConfig = _configManager.Get<DevOpsServerConfig>();
                var urlChanged = devOpsServerConfig.Url != _configurationViewModel.DevOpsServerUrl;
                var ageChanged = devOpsServerConfig.AgeOfBuilds != _configurationViewModel.SelectedBuildAge;
                devOpsServerConfig.Url = _configurationViewModel.DevOpsServerUrl;
                devOpsServerConfig.BuildRefreshTime = _configurationViewModel.SelectedRefreshTime;
                devOpsServerConfig.AgeOfBuilds = _configurationViewModel.SelectedBuildAge;

                var completeRefresh = urlChanged || ageChanged;
                _buildsOverviewViewModel.RestartTimer(completeRefresh);

                var autostartConfig = _configManager.Get<AutostartConfig>();
                autostartConfig.Enabled = _configurationViewModel.AutostartEnabled;
                autostartConfig.Hidden = _configurationViewModel.AutostartHidden;
                AutoStartHelper.SetAutoStart(autostartConfig.Enabled, autostartConfig.Hidden);
            }

            _pageViewModel.CurrentPage = Page.Builds;
        }

        public event EventHandler CanExecuteChanged;
    }
}