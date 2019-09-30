using System;
using System.Windows.Input;
using System.Windows.Markup;
using Kingfisher.WpfViewModels;

namespace Kingfisher.Commands
{
    public class HideMainWindowCommand : MarkupExtension, ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainWindowViewModel.Instance.Hide();
        }

        public event EventHandler CanExecuteChanged;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}