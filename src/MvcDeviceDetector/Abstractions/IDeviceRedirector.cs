using Microsoft.AspNetCore.Http;

namespace MvcDeviceDetector.Abstractions
{

    public interface IDeviceRedirector
    {
        void RedirectToDevice(HttpContext context, string code = "");
    }
}