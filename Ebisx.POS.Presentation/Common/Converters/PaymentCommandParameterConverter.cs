using System.Globalization;

namespace Ebisx.POS.Presentation.Common.Converters;

public class PaymentCommandParameterConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return new Tuple<object, object>(value, parameter);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
