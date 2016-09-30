namespace MvcDeviceDetector.Abstractions
{
	#region usings

	using Microsoft.AspNetCore.Http;

	#endregion

	public interface IDeviceResolver
	{
		IDevice ResolveDevice(HttpContext context);
	}
}