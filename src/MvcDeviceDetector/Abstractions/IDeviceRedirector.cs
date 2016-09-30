namespace MvcDeviceDetector.Abstractions
{
	#region usings

	using Microsoft.AspNetCore.Http;

	#endregion

	public interface IDeviceRedirector
	{
		void RedirectToDevice(HttpContext context, string code = "");
	}
}