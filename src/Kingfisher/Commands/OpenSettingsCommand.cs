using System;
using System.Windows.Input;
using Kingfisher.Provider.Configuration;
using Kingfisher.ViewModels;
using Kingfisher.WpfViewModels;

namespace Kingfisher.Commands
{
    public class OpenSettingsCommand : ICommand
    {
        private readonly IConfigManager _configManager;
        private readonly ConfigurationViewModel _configurationViewModel;
        private readonly PageViewModel _pageViewModel;

        public OpenSettingsCommand(PageViewModel pageViewModel, ConfigurationViewModel configurationViewModel, IConfigManager configManager)
        {
            _pageViewModel = pageViewModel;
            _configurationViewModel = configurationViewModel;
            _configManager = configManager;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var serverConfig = _configManager.Get<DevOpsServerConfig>();
            // this will trigger checking if the url is valid
            _configurationViewModel.DevOpsServerUrl = serverConfig.Url;
            _configurationViewModel.SelectedRefreshTime = serverConfig.BuildRefreshTime;
            _configurationViewModel.SelectedBuildAge = serverConfig.AgeOfBuilds;

            var autostartConfig = _configManager.Get<AutostartConfig>();
            _configurationViewModel.AutostartEnabled = autostartConfig.Enabled;
            _configurationViewModel.AutostartHidden = autostartConfig.Hidden;


            // now switch to configuration page.
            _pageViewModel.CurrentPage = Page.Configuration;
            MainWindowViewModel.Instance.Show();
        }


        public event EventHandler CanExecuteChanged;
    }
}