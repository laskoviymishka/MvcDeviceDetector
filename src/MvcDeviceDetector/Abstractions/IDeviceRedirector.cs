namespace MvcDeviceDetector.Abstractions
{
	using Microsoft.AspNetCore.Http;

	public interface IDeviceRedirector
    {
        void RedirectToDevice(HttpContext context, string code = "");
    }
}