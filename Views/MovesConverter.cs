using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace BatoBatoPick.Views
{
    public  class MovesConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int IntVal) 
            {
                if (IntVal == 1) return "Rock";
                if (IntVal == 2) return "Paper";
                if (IntVal == 3) return "Scissors";
            } return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
