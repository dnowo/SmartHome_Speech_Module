
using SHSM.Devices;
using System.Collections.Generic;

namespace SHSM {
    public partial class Home
    {
        public List<Device> devices;

        public Home()
        {
            InitializeComponent();
        }

        public void SetDevices(List<Device> devices)
        { 
            this.devices = devices;
        }
    }
}