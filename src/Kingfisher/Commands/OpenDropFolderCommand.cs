using System;
using System.Diagnostics;
using System.Windows.Input;
using Kingfisher.Provider;
using Kingfisher.Provider.Utils;
using Kingfisher.ViewModels;

namespace Kingfisher.Commands
{
    public class OpenDropFolderCommand : ICommand
    {
        private readonly IBuildsProvider _buildsProvider;
        private readonly IExceptionViewModel _exceptionViewModel;
        
        public OpenDropFolderCommand(IExceptionViewModel exceptionViewModel, IBuildsProvider buildsProvider)
        {
            _exceptionViewModel = exceptionViewModel;
            _buildsProvider = buildsProvider;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (!(parameter is BuildViewModel build))
                return;

            try
            {
                var filePath = await _buildsProvider.GetDropFolderPathAsync(build.Project, build.Id);

                if (string.IsNullOrEmpty(filePath))
                    return;

                ProcessHelper.Start(filePath);
            }
            catch (Exception ex)
            {
                _exceptionViewModel.Exception = ex;
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}