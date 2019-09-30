using System;
using System.Globalization;
using System.Windows.Data;

namespace Kingfisher.Converters
{
    // Couldn't get the StringFormat= extension in XAML to work, it never used the format I specified.

    public class SortableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
                return FormatDate(date);

            if (value is TimeSpan time)
                return FormatTime(time);

            return value;
        }

        private object FormatDate(DateTime date)
        {
            return date.ToString("g");
        }

        private object FormatTime(TimeSpan time)
        {
            return time.ToString("g");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}