using System;
using System.CodeDom;
using System.Resources;
using System.Windows.Markup;

namespace Kingfisher.MarkupExtensions
{
    public class Translate : MarkupExtension
    {
        public Type Type { get; set; }

        public string Key { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var rm = new ResourceManager(Type.FullName, Type.Assembly);

            return rm.GetString(Key) ?? $"[[{Key}]]";
        }
    }
}