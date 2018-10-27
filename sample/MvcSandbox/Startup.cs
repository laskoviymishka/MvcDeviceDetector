namespace MvcSandbox
{
	#region usings

	using System.IO;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using MvcDeviceDetector;
	using MvcDeviceDetector.Abstractions;
	using MvcDeviceDetector.Preference;

	#endregion

	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvcCore().AddRazorViewEngine();
			services.AddTransient<ISitePreferenceRepository, SitePreferenceRepository>();
			services.AddDeviceSwitcher<UrlSwitcher>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
		{
			app.UseMvc(routes =>
			{
				routes.MapDeviceSwitcher();
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseKestrel()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}