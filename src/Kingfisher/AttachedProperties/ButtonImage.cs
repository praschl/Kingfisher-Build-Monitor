using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kingfisher.AttachedProperties
{
    public class ButtonImage
    {
        public static readonly DependencyProperty ImageStyleProperty = DependencyProperty.RegisterAttached(
            "ImageStyle", 
            typeof(Style), 
            typeof(ButtonImage), 
            new PropertyMetadata(default(Style)));

        public static void SetImageStyle(DependencyObject element, Style value)
        {
            element.SetValue(ImageStyleProperty, value);
        }

        public static Style GetImageStyle(DependencyObject element)
        {
            return (Style) element.GetValue(ImageStyleProperty);
        }
    }
}