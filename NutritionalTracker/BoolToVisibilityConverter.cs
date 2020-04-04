using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NutritionalTracker
{
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public bool Collapse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is bool)) {
                return Visibility.Visible;
            }

            var boolValue = (bool) value;

            if(boolValue) {
                return Visibility.Visible;
            }

            return Collapse ? Visibility.Collapsed : (object) Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}