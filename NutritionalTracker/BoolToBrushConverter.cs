using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NutritionalTracker {
    public sealed class BoolToBrushConverter : IValueConverter {
        public Brush BrushIfTrue { get; set; }
        public Brush BrushIfFalse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is bool)) {
                return BrushIfFalse;
            }

            var boolValue = (bool) value;

            return boolValue ? BrushIfTrue : BrushIfFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}