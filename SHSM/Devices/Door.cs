using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;

namespace SHSM.Devices
{
    public class Door : Device
    {
        public Door()
        {
            Type = Type.DOOR;
        }
    }
}
