using Microsoft.AspNetCore.Http;

namespace MvcDeviceDetector.Abstractions
{

    public interface ISitePreferenceRepository
    {
        IDevice LoadPreference(HttpContext context);
        void SavePreference(HttpContext context, IDevice device);
        void ResetPreference(HttpContext context);
    }
}