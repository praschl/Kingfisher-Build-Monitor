using System;
using System.Globalization;
using System.Windows.Data;
using Humanizer;

namespace Kingfisher.Converters
{
    public class ReadableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
                return FormatDate(date);

            if (value is TimeSpan time)
                return FormatTime(time);

            return value;
        }

        private string FormatDate(in DateTime date)
        {
            if (date.Date == DateTime.Today)
                return string.Format(CultureInfo.CurrentCulture, FormatResources.Today_Format, FormatTime(DateTime.Now - date));

            if (date.Date >= DateTime.Today.AddDays(-1))
                return string.Format(CultureInfo.CurrentCulture, FormatResources.Yesterday_Format, date.ToString("t"));

            if (date.Date >= DateTime.Today.AddDays(-6))
                return string.Format(CultureInfo.CurrentCulture, FormatResources.LastWeek_Format, date.ToString("ddd"), date.ToString("t"));

            if (date.Year == DateTime.Today.Year)
                return date.ToString("M");

            return date.ToString("Y");
        }
        
        private string FormatTime(TimeSpan time)
        {
            if (time.TotalSeconds < 60)
                return string.Format(CultureInfo.CurrentCulture, FormatResources.Short_Seconds, time);

            if (time.TotalMinutes < 60)
                return string.Format(CultureInfo.CurrentCulture, FormatResources.Short_Minutes, time);

            if (time.TotalHours < 24)
                return string.Format(CultureInfo.CurrentCulture, FormatResources.Short_Hours, time);

            return string.Format(CultureInfo.CurrentCulture, FormatResources.Short_Days, time);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}