using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Kingfisher.Controls
{
    public partial class ExceptionDisplay : UserControl
    {
        public static readonly DependencyProperty ExceptionProperty = DependencyProperty.Register(
            nameof(Exception),
            typeof(Exception),
            typeof(ExceptionDisplay),
            new FrameworkPropertyMetadata(default(Exception))
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public ExceptionDisplay()
        {
            InitializeComponent();
        }

        public Exception Exception
        {
            get => (Exception) GetValue(ExceptionProperty);
            set => SetValue(ExceptionProperty, value);
        }

        // TODO: make these a command (?)
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Exception = null;
        }

        private void CopyButton_OnClick(object sender, RoutedEventArgs e)
        {
            var exceptionText = Exception?.ToString();
            Clipboard.SetText(exceptionText ?? string.Empty);
        }
    }
}