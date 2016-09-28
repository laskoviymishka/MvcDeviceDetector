namespace MvcDeviceDetector.Abstractions
{
	using Microsoft.AspNetCore.Http;

	public interface IDeviceResolver
    {
        IDevice ResolveDevice(HttpContext context);
    }
}