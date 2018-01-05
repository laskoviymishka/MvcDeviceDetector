using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MvcDeviceDetector.Abstractions;

namespace MvcDeviceDetector.Preference
{
    public class SitePreferenceRepository : ISitePreferenceRepository
    {
        private readonly IDeviceResolver _deviceResolver;
        private readonly IOptions<SwitcherOptions> _options;
        private readonly IEnumerable<IDeviceSwitcher> _switchers;

        public SitePreferenceRepository(IEnumerable<IDeviceSwitcher> switchers, IOptions<SwitcherOptions> options,
            IDeviceResolver deviceResolver)
        {
            _switchers = switchers;
            _options = options;
            _deviceResolver = deviceResolver;
        }

        public IDevice LoadPreference(HttpContext context)
            => _switchers
                .OrderByDescending(t => t.Priority)
                .Select(t => t.LoadPreference(context))
                .FirstOrDefault(t => t != null) ?? _deviceResolver.ResolveDevice(context);

        public void ResetPreference(HttpContext context) => _options.Value.DefaultSwitcher.ResetStore(context);

        public void SavePreference(HttpContext context, IDevice device)
            => _options.Value.DefaultSwitcher.StoreDevice(context, device);
    }
}