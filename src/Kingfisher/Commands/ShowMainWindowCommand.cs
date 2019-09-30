using System;
using System.Windows.Input;
using Kingfisher.ViewModels;
using Kingfisher.WpfViewModels;

namespace Kingfisher.Commands
{
    public class ShowMainWindowCommand : ICommand
    {
        private readonly PageViewModel _pageViewModel;

        public ShowMainWindowCommand(PageViewModel pageViewModel)
        {
            _pageViewModel = pageViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _pageViewModel.CurrentPage = Page.Builds;
            MainWindowViewModel.Instance.Show();
        }

        public event EventHandler CanExecuteChanged;
    }
}