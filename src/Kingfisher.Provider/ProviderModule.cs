using System;
using Autofac;
using Kingfisher.Provider.Configuration;

namespace Kingfisher.Provider
{
    public class ProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BuildsProvider>().AsImplementedInterfaces();
            builder.RegisterType<DevOpsApiProvider>().AsImplementedInterfaces();

            // we can inject a Func<IDevOpsApi> and always get the one for the uri set here
            // with one of the next changes we will remove the constant uri and put it in a config
            builder.Register(c =>
            {
                var uri = c.Resolve<IConfigManager>().Get<DevOpsServerConfig>().Url;
                if (string.IsNullOrEmpty(uri))
                    throw new ArgumentNullException("DevOpsServerServer.Url", "Uri for devops server is not configured.");

                var provider = c.Resolve<IDevOpsApiProvider>();
                var api = provider.GetDevOpsApi(uri);
                return api;
            });
        }
    }
}