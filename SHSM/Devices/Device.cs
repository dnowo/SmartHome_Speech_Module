using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;

namespace SHSM.Devices
{
    public abstract class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public Place Place { get; set; }
        public bool State { get; set; }

        [NotMapped]
        public Image Image { get; set; }
    }
}
