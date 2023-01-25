using System.Windows.Data;
using System.Windows.Media;
using SHSM.Devices;
using Type = System.Type;

namespace SHSM.Converters
{
    [ValueConversion(typeof(Device), typeof(ImageSource))]
    public class DeviceToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Device device = (Device)value;
            return device.State ? device.ImageOn.Source : device.ImageOff.Source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}