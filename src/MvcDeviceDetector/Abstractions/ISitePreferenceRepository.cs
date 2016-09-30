namespace MvcDeviceDetector.Abstractions
{
	#region usings

	using Microsoft.AspNetCore.Http;

	#endregion

	public interface ISitePreferenceRepository
	{
		IDevice LoadPreference(HttpContext context);
		void SavePreference(HttpContext context, IDevice device);
		void ResetPreference(HttpContext context);
	}
}