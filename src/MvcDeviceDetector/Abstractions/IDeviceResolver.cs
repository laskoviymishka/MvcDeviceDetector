using Microsoft.AspNetCore.Http;

namespace MvcDeviceDetector.Abstractions
{

    public interface IDeviceResolver
    {
        IDevice ResolveDevice(HttpContext context);
    }
}