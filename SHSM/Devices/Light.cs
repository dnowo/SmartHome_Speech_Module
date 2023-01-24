using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SHSM.Devices
{
    public class Light : Device
    {
        public int NumericalState { get; set; }

        public Light()
        {
            Type = Type.LIGHT;
            Image = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("C:\\Users\\Daniel\\Desktop\\SWPSD\\SmartHome_Speech_Module\\SHSM\\Resources\\light-bulb.png");
            bi.EndInit();
            Image.Source = bi;
        }
    }
}
