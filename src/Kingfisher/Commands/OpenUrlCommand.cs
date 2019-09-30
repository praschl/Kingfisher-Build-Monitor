using System;
using System.Windows.Input;
using Kingfisher.Provider.Utils;

namespace Kingfisher.Commands
{
    public class OpenUrlCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is string url) || string.IsNullOrEmpty(url))
                return;

            ProcessHelper.Start(url);
        }

        public event EventHandler CanExecuteChanged;
    }
}