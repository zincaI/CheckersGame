using Checkers.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Checkers.Converters
{
    internal class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is colorpiece color)
            {
                switch (color)
                {
                    case colorpiece.Red:
                        return Brushes.Red;
                    case colorpiece.Black:
                        return Brushes.Black;
                    case colorpiece.Green:
                        return Brushes.Green;
                    default:
                        return Brushes.Red; 
                }
            }
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is colorpiece color)
            {
                switch (color)
                {
                    case colorpiece.Red:
                        return Brushes.Red;
                    case colorpiece.Black:
                        return Brushes.Black;
                    case colorpiece.Green:
                        return Brushes.Green;
                    default:
                        return Brushes.Red;
                }
            }
            return Brushes.Red;
        }
    }
}
