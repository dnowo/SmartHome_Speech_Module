using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SHSM.Devices
{
    public class Door : Device
    {
        public Door()
        {
            Type = Type.DOOR;

            ImageOn = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"/SHSM;component/Resources/door_opened.png", UriKind.Relative);
            bi.EndInit();
            ImageOn.Visibility = Visibility.Hidden;
            ImageOn.Source = bi;

            ImageOff = new Image();
            bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"/SHSM;component/Resources/door_closed.png", UriKind.Relative);
            bi.EndInit();
            ImageOff.Visibility = Visibility.Hidden;
            ImageOff.Source = bi;
        }
    }
}
