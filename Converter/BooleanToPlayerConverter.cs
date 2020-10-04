using System;
using System.Globalization;
using System.Windows.Data;

namespace Gomoku.Converter
{
    public class BooleanToPlayerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool && (bool)value)
            {
                return "Black";
            }
            return "White";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
