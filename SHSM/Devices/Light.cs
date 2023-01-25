using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SHSM.Devices
{
    public class Light : Device
    {
        private int _numericalState;
        public int NumericalState
        {
            get => _numericalState;
            set { _numericalState = value; OnPropertyChanged(); }
        }

        public Light()
        {
            Type = Type.LIGHT;

            ImageOn = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"/SHSM;component/Resources/light_on.png", UriKind.Relative);
            bi.EndInit();
            ImageOn.Source = bi;

            ImageOff = new Image();
            bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"/SHSM;component/Resources/light_off.png", UriKind.Relative);
            bi.EndInit();
            ImageOff.Source = bi;
        }
    }
}
