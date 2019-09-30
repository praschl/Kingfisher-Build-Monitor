//using System.Windows;
//using Kingfisher.ViewModels;

//namespace Kingfisher
//{
//    /// <summary>
//    ///     This class helps binding to models, when the UI element is not within the required visual tree, and has not the
//    ///     correct datacontext set.
//    ///     It can also be useful when binding to an object that is more than just one property away or when binding to a
//    ///     property that is farer up the DataContext
//    /// </summary>
//    public class BindingProxy : Freezable
//    {
//        public static readonly DependencyProperty ValueProperty =
//            DependencyProperty.Register(nameof(Value), typeof(BuildsOverviewViewModel), typeof(BindingProxy), new UIPropertyMetadata(null));

//        public BuildsOverviewViewModel Value
//        {
//            get => (BuildsOverviewViewModel) GetValue(ValueProperty);
//            set => SetValue(ValueProperty, value);
//        }

//        protected override Freezable CreateInstanceCore()
//        {
//            return new BindingProxy();
//        }
//    }
//}