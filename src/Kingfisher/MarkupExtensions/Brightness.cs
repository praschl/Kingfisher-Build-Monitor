using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace Kingfisher.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(Color))]
    public class Brightness : MarkupExtension
    {
        public Color Color { get; set; }

        public double Amount { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ChangeColorBrightness(Color, Amount);
        }

        // https://stackoverflow.com/a/12598573/886117
        private static Color ChangeColorBrightness(Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }
    }
}
