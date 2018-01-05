using Microsoft.AspNetCore.Http;

namespace MvcDeviceDetector.Abstractions
{

    public interface IDeviceSwitcher
    {
        int Priority { get; }
        IDevice LoadPreference(HttpContext context);
        void StoreDevice(HttpContext context, IDevice device);
        void ResetStore(HttpContext context);
    }
}