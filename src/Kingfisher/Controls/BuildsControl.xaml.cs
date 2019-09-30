using System.Windows.Controls;
using Kingfisher.ViewModels;

namespace Kingfisher.Controls
{
    public partial class BuildsControl : UserControl
    {
        public BuildsOverviewViewModel Model { get; set; }

        public BuildsControl(BuildsOverviewViewModel buildsOverviewViewModel)
        {
            Model = buildsOverviewViewModel;

            InitializeComponent();
        }
    }
}