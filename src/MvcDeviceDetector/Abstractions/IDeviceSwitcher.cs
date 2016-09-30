namespace MvcDeviceDetector.Abstractions
{
	#region usings

	using Microsoft.AspNetCore.Http;

	#endregion

	public interface IDeviceSwitcher
	{
		int Priority { get; }
		IDevice LoadPreference(HttpContext context);
		void StoreDevice(HttpContext context, IDevice device);
		void ResetStore(HttpContext context);
	}
}