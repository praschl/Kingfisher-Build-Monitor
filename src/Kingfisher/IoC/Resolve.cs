using System;
using System.Windows.Markup;
using Autofac;

namespace Kingfisher.IoC
{
    public class Resolve : MarkupExtension
    {
        public Type ServiceType { get; set; }
        public string Name { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {   
            if (string.IsNullOrEmpty(Name))
                return ServiceLocator.Instance.Resolve(ServiceType);

            return ServiceLocator.Instance.ResolveNamed(Name, ServiceType);
        }
    }
}