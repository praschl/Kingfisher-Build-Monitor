using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Humanizer;
using Humanizer.Localisation;

namespace Kingfisher.Converters
{
    public class HumanizerConverter : MarkupExtension, IValueConverter
    {
        public int Precision { get; set; } = 1;

        public TimeUnit MaxUnit { get; set; } = TimeUnit.Year;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan time))
                return value?.ToString();

            return time.Humanize(Precision, maxUnit: MaxUnit);
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