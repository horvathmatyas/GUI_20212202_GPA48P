using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SpaceBaloons.Converter
{
    internal class NumberToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = int.Parse(value.ToString());
            if (number <= 20)
            {
                return Brushes.Red;
            }
            else if (number <= 40)
            {
                return Brushes.Orange;
            }
            else if (number <= 60)
            {
                return Brushes.Yellow;
            }
            else if (number <= 80)
            {
                return Brushes.LightGreen;
            }
            else
            {
                return Brushes.DarkGreen;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
