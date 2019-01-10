using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TransportSchedule.UI
{
    class ProgressToWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var minimum = (double)values[0];
            var maximum = (double)values[1];
            var value = (double)values[2];
            var width = (double)values[3];
            var part = (value - minimum) / (maximum - minimum);
            return new GridLength(part * width);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
