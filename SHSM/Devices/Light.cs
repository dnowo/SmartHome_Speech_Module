using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;

namespace SHSM.Devices
{
    public class Light : Device
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public override string Type { get; set; }
        public override string Place { get; set; }
        public override bool State { get; set; }

        [NotMapped]
        public override Image Image { get; set; }

        public int NumericalState { get; set; }

        public Light()
        {
            Type = "light";
        }
    }
}
