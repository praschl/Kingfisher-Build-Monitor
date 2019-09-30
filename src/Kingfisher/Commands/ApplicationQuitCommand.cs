using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace Kingfisher.Commands
{
    public class ApplicationQuitCommand : MarkupExtension, ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }

        public event EventHandler CanExecuteChanged;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}