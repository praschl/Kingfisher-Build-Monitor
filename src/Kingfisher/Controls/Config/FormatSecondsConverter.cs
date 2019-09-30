using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Humanizer;

namespace Kingfisher.Controls.Config
{
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class FormatSecondsConverter : MarkupExtension, IValueConverter
    {
        public string StringFormat { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var asInt = System.Convert.ToInt32(value);

            var time = TimeSpan.FromSeconds(asInt);

            return string.Format(StringFormat, time.Humanize(2));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}