using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHSM.Devices
{
    public enum Place
    {
        [Description("kitchen")]
        KITCHEN,
        [Description("livingroom")]
        LIVINGROOM,
        [Description("dayroom")]
        DAYROOM,
        [Description("bathroom")]
        BATHROOM,
        [Description("hall")]
        HALL
    }
}
