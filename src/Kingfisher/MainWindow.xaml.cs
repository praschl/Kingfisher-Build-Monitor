using System.ComponentModel;
using System.Windows;
using Kingfisher.WpfViewModels;

namespace Kingfisher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowViewModel = new MainWindowViewModel(this);

            InitializeComponent();
        }

        public MainWindowViewModel WindowViewModel { get; set; }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
