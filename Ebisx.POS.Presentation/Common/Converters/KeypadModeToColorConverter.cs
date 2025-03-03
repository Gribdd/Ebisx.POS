using Ebisx.POS.Presentation.Common.Enums;
using System.Globalization;
using Microsoft.Maui.Graphics;

namespace Ebisx.POS.Presentation.Common.Converters;

public  class KeypadModeToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is KeypadMode mode && parameter is string paramMode)
        {
            return mode.ToString() == paramMode ? Colors.Green : Colors.Transparent;
        }
        return Colors.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
