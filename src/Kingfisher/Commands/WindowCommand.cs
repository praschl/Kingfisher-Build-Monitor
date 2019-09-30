using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using Kingfisher.WpfViewModels;

namespace Kingfisher.Commands
{
    public class WindowCommand : MarkupExtension, ICommand
    {
        public enum CommandType
        {
            Close,
            Minimize,
            Maximize,
            Menu
        }

        public CommandType Command { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is Window window))
                return;

            switch (Command)
            {
                case CommandType.Close:
                    window.Close();
                    return;

                case CommandType.Maximize:
                    window.WindowState ^= WindowState.Maximized;
                    return;

                case CommandType.Minimize:
                    window.WindowState = WindowState.Minimized;
                    return;

                case CommandType.Menu:
                    var point = window.PointToScreen(MainWindowViewModel.GetMenuPosition());

                    var dpiInfo = VisualTreeHelper.GetDpi(window);

                    point.X /= dpiInfo.DpiScaleX;
                    point.Y /= dpiInfo.DpiScaleY;

                    SystemCommands.ShowSystemMenu(window, point);
                    return;

                default:
                    Debug.WriteLine($"{Command} is not a valid command type.");
                    return;
            }
        }

        public event EventHandler CanExecuteChanged;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}