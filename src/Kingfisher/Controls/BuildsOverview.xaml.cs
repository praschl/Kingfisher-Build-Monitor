using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Kingfisher.Controls
{
    public partial class BuildsOverview : UserControl
    {
        public static readonly DependencyProperty MyBuildsProperty = DependencyProperty.Register(
            nameof(Builds),
            typeof(ICollectionView),
            typeof(BuildsOverview),
            new PropertyMetadata(default(ICollectionView)));

        public ICollectionView Builds
        {
            get { return (ICollectionView) GetValue(MyBuildsProperty); }
            set { SetValue(MyBuildsProperty, value); }
        }

        public BuildsOverview()
        {
            InitializeComponent();
        }
    }
}