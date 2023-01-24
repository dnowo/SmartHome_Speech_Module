using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;

namespace SHSM.Devices
{
    public abstract class Device
    {
        public abstract int Id { get; set; }
        public abstract string Name { get; set; }
        public abstract string Type { get; set; }
        public abstract string Place { get; set; }
        public abstract bool State { get; set; }

        [NotMapped]
        public abstract Image Image { get; set; }
    }
}
