using MvcDeviceDetector.Abstractions;

namespace MvcDeviceDetector
{

    public class SwitcherOptions
    {
        public string MobileKey { get; set; } = "mobile";
        public string TabletKey { get; set; } = "tablet";
        public string NormalKey { get; set; } = "normal";
        public string ResetKey { get; set; } = "reset";
        public IDeviceSwitcher DefaultSwitcher { get; set; }
        public string SwitchUrl { get; set; } = "choose";
    }
}