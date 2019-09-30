using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Humanizer;
using Humanizer.Localisation;

namespace Kingfisher.Controls.Config
{
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class AgeOfBuildFormatConverter : MarkupExtension, IValueConverter
    {
        public string StringFormat { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var asInt = System.Convert.ToInt32(value);

            var time = TimeSpan.FromDays(asInt);

            return string.Format(StringFormat, time.Humanize(1, maxUnit: TimeUnit.Day));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}