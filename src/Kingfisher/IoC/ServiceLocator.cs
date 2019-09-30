using System.Windows.Controls;
using Autofac;
using Kingfisher.Commands;
using Kingfisher.Controls;
using Kingfisher.Controls.Config;
using Kingfisher.Controls.Pages;
using Kingfisher.Provider;
using Kingfisher.Provider.Configuration;
using Kingfisher.ViewModels;
using Kingfisher.ViewModels.Mappers;
using Kingfisher.WpfViewModels;

namespace Kingfisher.IoC
{
    public static class ServiceLocator
    {
        public static IContainer Instance { get; } = InitContainer();

        private static IContainer InitContainer()
        {
            var config = JsonConfigManager.Default;

            var builder = new ContainerBuilder();
            
            // defaults
            builder.RegisterInstance(config).AsImplementedInterfaces();
            builder.RegisterType<ProjectMapper>().AsImplementedInterfaces();
            builder.RegisterType<BuildsMapper>().AsImplementedInterfaces();

            // windows
            builder.RegisterType<MainWindow>().SingleInstance().AsSelf();

            // controls
            builder.RegisterType<PageControl>().AsSelf();
            builder.RegisterType<BuildsControl>().Named<Control>("Builds");
            builder.RegisterType<ConfigurationControl>().Named<Control>("Configuration");

            // viewmodel
            builder.RegisterType<PageViewModel>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<BuildsOverviewViewModel>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ConfigurationViewModel>().AsSelf().AsImplementedInterfaces().SingleInstance();

            // commands
            builder.RegisterType<ShowMainWindowCommand>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<OpenDropFolderCommand>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<OpenSettingsCommand>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CloseSettingsCommand>().AsSelf().AsImplementedInterfaces().SingleInstance();

            // module
            builder.RegisterModule<ProviderModule>();

            return builder.Build();
        }

    }
}
