using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Kingfisher.AttachedProperties
{
    public class SpinAnimation
    {
        public static readonly DependencyProperty SpeedProperty = DependencyProperty.RegisterAttached(
            "Speed",
            typeof(int),
            typeof(SpinAnimation),
            new PropertyMetadata(default(int),
                SpeedChangedCallback));

        private static void SpeedChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FrameworkElement element))
                return;

            element.Loaded += (o, ev) => Element_Loaded(element, (int) e.NewValue);
        }

        private static void Element_Loaded(FrameworkElement element, int speed)
        {
            var rotation = new RotateTransform();
            element.RenderTransform = rotation;
            if (element.RenderTransformOrigin.Equals(new Point(0,0)))
                element.RenderTransformOrigin = new Point(0.5, 0.5);

            var animation = new DoubleAnimation(0, 360, TimeSpan.FromSeconds(1.0 / speed));
            animation.RepeatBehavior = RepeatBehavior.Forever;
            Timeline.SetDesiredFrameRate(animation, 8 * speed);

            rotation.BeginAnimation(RotateTransform.AngleProperty, animation);
        }

        public static void SetSpeed(DependencyObject element, int value)
        {
            element.SetValue(SpeedProperty, value);
        }

        public static int GetSpeed(DependencyObject element)
        {
            return (int) element.GetValue(SpeedProperty);
        }
    }
}