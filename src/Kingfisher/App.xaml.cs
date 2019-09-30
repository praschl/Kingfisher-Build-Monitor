using System.Linq;
using System.Windows;
using Autofac;
using Kingfisher.Exceptions;
using Kingfisher.IoC;
using Kingfisher.Provider.Configuration;
using Kingfisher.Provider.SingleInstance;
using Kingfisher.ViewModels;

namespace Kingfisher
{
    public partial class App : Application
    {
        private IConfigManager _configManager;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            GlobalExceptionHandler.Setup(this);

            if (!SingleInstanceApp.IsSingleInstance("Kingfisher-DevOps-Monitor"))
            {
                Shutdown();
                return;
            }
            SingleInstanceApp.SecondInstanceStarted += SingleContextSecondInstanceStarted;

            _configManager = ServiceLocator.Instance.Resolve<IConfigManager>();

            var model = ServiceLocator.Instance.Resolve<BuildsOverviewViewModel>();
            var mainWindow = ServiceLocator.Instance.Resolve<MainWindow>();

            model.RestartTimer();

            ShowMainWindow(mainWindow, e.Args.Contains("-hidden"));
        }

        private void SingleContextSecondInstanceStarted(object sender, SingleInstanceEventArgs e)
        {
            Dispatcher?.Invoke(() =>
            {
                var mainWindow = ServiceLocator.Instance.Resolve<MainWindow>();
                mainWindow.Show();
            });
        }

        private void ShowMainWindow(MainWindow mainWindow, bool hidden)
        {
            if (hidden)
                mainWindow.Hide();
            else
                mainWindow.Show();
        }

        private void Application_OnExit(object sender, ExitEventArgs e)
        {
            // TODO: stop timer and wait for pending tasks, or a TaskCanceledException might happen.

            _configManager?.SaveAll();
            SingleInstanceApp.Cleanup();
        }
    }
}