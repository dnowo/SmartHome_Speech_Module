using System.ComponentModel;

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
