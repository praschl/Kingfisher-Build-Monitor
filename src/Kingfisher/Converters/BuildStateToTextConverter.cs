using System;
using System.Globalization;
using System.Windows.Data;
using Kingfisher.ViewModels;

namespace Kingfisher.Converters
{
    public class BuildStateToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is BuildState state))
                return value?.ToString();

            return state switch
            {
                BuildState.inProgress => "in progress",
                BuildState.notStarted => "not started",
                BuildState.partiallySucceeded => "partially succeeded",
                _ => state.ToString()
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}