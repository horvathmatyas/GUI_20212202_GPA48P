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
    public class NumberToColorConverter 
    {
        public Brush Convert(int value)
        {
            if (value <= 20)
            {
                return Brushes.Red;
            }
            else if (value <= 40)
            {
                return Brushes.Orange;
            }
            else if (value <= 60)
            {
                return Brushes.Yellow;
            }
            else if (value <= 80)
            {
                return Brushes.LightGreen;
            }
            else
            {
                return Brushes.DarkGreen;
            }
        }

  
    }
}
