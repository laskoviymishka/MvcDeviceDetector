using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MvcDeviceDetector.Abstractions;
using MvcDeviceDetector.Device;
using MvcDeviceDetector.Preference;

namespace MvcDeviceDetector
{

    public static class Configuration
    {
        public static IRouteBuilder MapDeviceSwitcher(this IRouteBuilder route)
            => route.MapDeviceSwitcher<PreferenceSwitcher>();

        public static IRouteBuilder MapDeviceSwitcher<TSwitcher>(this IRouteBuilder route)
            where TSwitcher : PreferenceSwitcher
            => route.MapGet(
                route.ServiceProvider.GetService<IOptions<SwitcherOptions>>().Value.SwitchUrl + "/{device}",
                route.ServiceProvider.GetRequiredService<TSwitcher>().Handle);

        public static IServiceCollection AddDeviceDetector(this IServiceCollection services,
            Action<DeviceOptions> device = null)
            => services.AddDeviceDetector<DefaultDeviceFactory>(device);

        public static IServiceCollection AddDeviceDetector<TDeviceFactory>(this IServiceCollection services,
            Action<DeviceOptions> device = null)
            where TDeviceFactory : class, IDeviceFactory
        {
            services.AddOptions();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddTransient<IDeviceFactory, TDeviceFactory>();
            services.AddTransient<IDeviceSwitcher, CookieSwitcher>();
            services.AddTransient<IDeviceSwitcher, UrlSwitcher>();

            services.TryAddTransient<IDeviceResolver, AgentResolver>();
            services.TryAddTransient<ISitePreferenceRepository, SitePreferenceRepository>();

            services.TryAddTransient<IDeviceAccessor, DeviceAccessor>();
            services.TryAddTransient<DeviceViewLocationExpander>();

            services.Configure(device ?? (options => { }));
            services.Configure<RazorViewEngineOptions>(
                options =>
                {
                    options.ViewLocationExpanders.Add(
                        services.BuildServiceProvider().GetService<DeviceViewLocationExpander>());
                });

            return services;
        }

        public static IServiceCollection AddDeviceSwitcher(
            this IServiceCollection services,
            Action<SwitcherOptions> switcher = null,
            Action<DeviceOptions> device = null)
            => services.AddDeviceSwitcher<CookieSwitcher>(switcher, device);

        public static IServiceCollection AddDeviceSwitcher<TPreference>(
            this IServiceCollection services,
            Action<SwitcherOptions> switcher = null,
            Action<DeviceOptions> device = null)
            where TPreference : class, IDeviceSwitcher
            => services.AddDeviceSwitcher<TPreference, DeviceRedirector>(switcher, device);

        public static IServiceCollection AddDeviceSwitcher<TPreference, TRedirector>(
            this IServiceCollection services,
            Action<SwitcherOptions> switcher = null,
            Action<DeviceOptions> device = null)
            where TPreference : class, IDeviceSwitcher
            where TRedirector : class, IDeviceRedirector
            => services.AddDeviceSwitcher<TPreference, TRedirector, DefaultDeviceFactory>(switcher, device);


        public static IServiceCollection AddDeviceSwitcher<TPreference, TRedirector, TDeviceFactory>(
            this IServiceCollection services,
            Action<SwitcherOptions> switcher = null,
            Action<DeviceOptions> device = null)
            where TPreference : class, IDeviceSwitcher
            where TRedirector : class, IDeviceRedirector
            where TDeviceFactory : class, IDeviceFactory
        {
            services.AddDeviceDetector<TDeviceFactory>(device);
            services.TryAddTransient<IDeviceRedirector, TRedirector>();
            services.TryAddTransient<TPreference>();
            services.TryAddTransient<PreferenceSwitcher>();
            services.Configure(
                switcher ??
                (options =>
                {
                    options.DefaultSwitcher =
                        services.BuildServiceProvider().GetRequiredService<TPreference>();
                }));

            return services;
        }
    }
}