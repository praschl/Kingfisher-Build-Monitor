using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Page = Kingfisher.ViewModels.Page;

namespace Kingfisher.Controls.Pages
{
    public class PageConverter : IValueConverter
    {
        public Control BuildsControl { get; set; }

        public Control ConfigurationControl { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Page page))
                return null;

            var control = FindControl(page);
            if (control == null)
            {
                return new TextBlock
                {
                    Text = $"PageConverter could not find a control for {page}",
                    Foreground = Brushes.Red
                };
            }

            return control;
        }

        private Control FindControl(Page page)
        {
            switch (page)
            {
                case Page.Builds:
                    return this.BuildsControl;
                case Page.Configuration:
                    return ConfigurationControl;
            }

            return null;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}