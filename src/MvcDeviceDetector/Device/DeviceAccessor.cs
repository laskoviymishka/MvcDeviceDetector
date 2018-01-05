using Microsoft.AspNetCore.Http;
using MvcDeviceDetector.Abstractions;

namespace MvcDeviceDetector.Device
{

    public class DeviceAccessor : IDeviceAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDeviceResolver _deviceResolver;
        private readonly ISitePreferenceRepository _repository;

        public DeviceAccessor(
            ISitePreferenceRepository repository,
            IHttpContextAccessor contextAccessor,
            IDeviceResolver deviceResolver)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            _deviceResolver = deviceResolver;
        }

        public IDevice Device => _deviceResolver.ResolveDevice(_contextAccessor.HttpContext);
        public IDevice Preference => _repository.LoadPreference(_contextAccessor.HttpContext);
    }
}