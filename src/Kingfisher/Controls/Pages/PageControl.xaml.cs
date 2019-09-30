using System.Windows.Controls;
using Kingfisher.ViewModels;

namespace Kingfisher.Controls.Pages
{
    public partial class PageControl : UserControl
    {
        public PageControl(PageViewModel pageViewModel)
        {
            PageViewModel = pageViewModel;

            InitializeComponent();
        }

        public PageViewModel PageViewModel { get; set; }
    }
}