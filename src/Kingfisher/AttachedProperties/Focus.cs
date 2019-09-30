using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kingfisher.AttachedProperties
{
    public class Focus
    {
        public static readonly DependencyProperty KeyProperty = DependencyProperty.RegisterAttached(
            "Key",
            typeof(Key),
            typeof(Focus),
            new PropertyMetadata(default(Key), KeyCallback));

        private static void KeyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Control control))
                return;

            var key = (Key) e.NewValue;

            void KeyHandler(object o, KeyEventArgs e)
            {
                if (e.Key == key) control.Focus();
            }

            void Loaded(object o1, RoutedEventArgs e1)
            {
                control.Loaded -= Loaded;

                var window = Window.GetWindow(d);
                if (window == null)
                {
                    Debug.WriteLine("Error setting Focus key: could not find window.");
                    return;
                }

                window.KeyUp += KeyHandler;
            }

            control.Loaded += Loaded;
        }

        public static void SetKey(DependencyObject element, Key value)
        {
            element.SetValue(KeyProperty, value);
        }

        public static Key GetKey(DependencyObject element)
        {
            return (Key) element.GetValue(KeyProperty);
        }
    }
}