using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHSM.Devices
{
    public enum Type
    {
        [Description("light")]
        LIGHT,
        [Description("door")]
        DOOR,
        [Description("window")]
        WINDOW
    }
}
