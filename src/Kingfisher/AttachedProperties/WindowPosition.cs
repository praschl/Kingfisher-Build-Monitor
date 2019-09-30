using System.Windows;
using Kingfisher.Config;
using Kingfisher.Provider.Configuration;

namespace Kingfisher.AttachedProperties
{
    public class WindowPosition
    {
        public static readonly DependencyProperty EnabledProperty = DependencyProperty.RegisterAttached(
            "Enabled",
            typeof(bool),
            typeof(WindowPosition),
            new PropertyMetadata(default(bool), EnabledChangedCallback));

        public static void SetEnabled(DependencyObject element, bool value)
        {
            element.SetValue(EnabledProperty, value);
        }

        public static bool GetEnabled(DependencyObject element)
        {
            return (bool) element.GetValue(EnabledProperty);
        }

        private static void EnabledChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window window))
                return;

            var config = JsonConfigManager.Default.Get<WindowPositionConfig>();

            if (config.Width>=window.MinWidth && config.Height>window.MinHeight)
            {
                window.Left = config.Left;
                window.Top = config.Top;
                window.Width = config.Width;
                window.Height = config.Height;
                window.WindowState = config.WindowState;
            }

            window.Loaded += (o1, e1) =>
            {
                window.LocationChanged += (o2, e2) => StoreNewSize(window, config);
                window.SizeChanged += (o2, e2) => StoreNewSize(window, config);
                window.StateChanged += (o2, e2) => StoreNewSize(window, config);
            };
        }

        private static void StoreNewSize(Window window, WindowPositionConfig config)
        {
            config.Left = window.RestoreBounds.Left;
            config.Top = window.RestoreBounds.Top;
            config.Width = window.RestoreBounds.Width;
            config.Height = window.RestoreBounds.Height;

            if (window.WindowState != WindowState.Minimized)
                config.WindowState = window.WindowState;
        }
    }
}