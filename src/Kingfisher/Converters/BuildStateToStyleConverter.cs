using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Kingfisher.ViewModels;

namespace Kingfisher.Converters
{
    public class BuildStateToStyleConverter : IValueConverter
    {
        public Style NotStarted { get; set; }
        public Style PostPoned { get; set; }
        public Style InProgress { get; set; }
        public Style Cancelling { get; set; }
        public Style Canceled { get; set; }
        public Style Succeeded { get; set; }
        public Style PartiallySucceeded { get; set; }
        public Style Failed { get; set; }
        public Style Unknown { get; set; }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is BuildState state))
                return Unknown;

            switch (state)
            {
                // states
                case BuildState.notStarted:
                    return NotStarted;
                case BuildState.postponed:
                    return PostPoned;
                case BuildState.inProgress:
                    return InProgress;
                case BuildState.cancelling:
                    return Cancelling;

                // results
                case BuildState.canceled:
                    return Canceled;
                case BuildState.succeeded:
                    return Succeeded;
                case BuildState.partiallySucceeded:
                    return PartiallySucceeded;
                case BuildState.failed:
                    return Failed;

                default:
                    return Unknown;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
