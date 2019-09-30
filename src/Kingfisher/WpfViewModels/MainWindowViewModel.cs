using System;
using System.ComponentModel;
using System.Windows;
using Kingfisher.External;
using Kingfisher.ViewModels;

namespace Kingfisher.WpfViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private const int ResizeBorder = 15;
        private static readonly PropertyChangedEventArgs _shadowThicknessPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(ShadowThickness));
        private static readonly PropertyChangedEventArgs _resizeBorderThicknessPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(ResizeBorderThickness));
        private readonly Window _window;
        private readonly WindowResizer _windowResizer;

        public static MainWindowViewModel Instance { get; private set; }

        public MainWindowViewModel(Window window)
        {
            if (Instance != null)
                throw new InvalidOperationException("MainWindowViewModel cannot be instantiated more than once.");
            Instance = this;

            _window = window;
            _window.SizeChanged += delegate
            {
                OnPropertyChanged(_shadowThicknessPropertyChangedEventArgs);
                OnPropertyChanged(_resizeBorderThicknessPropertyChangedEventArgs);
            };
            _windowResizer = new WindowResizer(window);
        }

        public Thickness ShadowThickness
        {
            get
            {
                // TODO: when docked to side, at least display the shadow on other side
                if (WindowState == WindowState.Maximized)
                    return _windowResizer.CurrentMonitorMargin; // when maximized, there still needs some border to be there for the taskbar

                if (IsDockedToSide)
                    return new Thickness(0); // when docked to side, the OS already took the taskbar into account.

                return new Thickness(ShadowWidth);
            }
        }

        public Thickness ResizeBorderThickness
        {
            get
            {
                if (WindowState == WindowState.Maximized)
                    return new Thickness(0);

                if (IsDockedToSide)
                    return new Thickness(ResizeBorder, 0, ResizeBorder, 0);

                return new Thickness(ResizeBorder);
            }
        }

        public bool IsDockedToSide => _window.Width != _window.RestoreBounds.Width || _window.Height != _window.RestoreBounds.Height;

        public WindowState WindowState { get; set; }

        public double ShadowWidth { get; set; } = 10;

        public Thickness BorderThickness { get; set; } = new Thickness(1, 30, 1, 1);

        public static Point GetMenuPosition()
        {
            return Instance?.GetMenuPositionCore() ?? new Point(0, 0);
        }

        private Point GetMenuPositionCore()
        {
            var left = ShadowWidth;
            var top = ShadowWidth;

            if (WindowState == WindowState.Maximized || IsDockedToSide)
            {
                left = _windowResizer.CurrentMonitorMargin.Left;
                top = _windowResizer.CurrentMonitorMargin.Top;
            }

            return new Point(left + BorderThickness.Left, top + BorderThickness.Top);
        }

        public void Show()
        {
            _window.Show();
            if (!_window.IsActive)
                _window.Activate();
            if (_window.WindowState == WindowState.Minimized)
                _window.WindowState = WindowState.Normal;
        }

        public void Hide()
        {
            _window.Hide();
        }

        public class Designer : MainWindowViewModel
        {
            public Designer() : base(null)
            {
            }

            public static Designer Instance => new Designer();
        }
    }
}