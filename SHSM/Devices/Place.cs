using System.ComponentModel;

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
        HALL,
        ALL
    }
}
