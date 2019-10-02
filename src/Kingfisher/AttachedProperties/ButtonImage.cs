using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FontAwesome5;

namespace Kingfisher.AttachedProperties
{
    public class ButtonImage
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
            "Icon", typeof(EFontAwesomeIcon), typeof(ButtonImage), new PropertyMetadata(default(EFontAwesomeIcon)));

        public static void SetIcon(DependencyObject element, EFontAwesomeIcon value)
        {
            element.SetValue(IconProperty, value);
        }

        public static EFontAwesomeIcon GetIcon(DependencyObject element)
        {
            return (EFontAwesomeIcon) element.GetValue(IconProperty);
        }

        public static readonly DependencyProperty IconBrushProperty = DependencyProperty.RegisterAttached(
            "IconBrush", typeof(Brush), typeof(ButtonImage), new PropertyMetadata(default(Brush)));

        public static void SetIconBrush(DependencyObject element, Brush value)
        {
            element.SetValue(IconBrushProperty, value);
        }

        public static Brush GetIconBrush(DependencyObject element)
        {
            return (Brush) element.GetValue(IconBrushProperty);
        }
    }
}