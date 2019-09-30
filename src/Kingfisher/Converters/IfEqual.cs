using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Kingfisher.Converters
{
    /// <summary>
    ///     A pretty readable (in xaml) converter for returning a value of arbitrary type
    ///     when two objects are equal (or not).
    /// </summary>
    public class IfEqual : MarkupExtension, IValueConverter
    {
        public object Test { get; set; }

        public object Then { get; set; }

        public object Else { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ReferenceEquals(Test, value))
                return Then;

            if (ReferenceEquals(Test, null) || ReferenceEquals(value, null))
                return Else;

            return value.Equals(Test) ? Then : Else;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}