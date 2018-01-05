using Microsoft.Extensions.Options;
using MvcDeviceDetector.Abstractions;

namespace MvcDeviceDetector.Device
{

    public class DefaultDeviceFactory : IDeviceFactory
    {
        private readonly IOptions<DeviceOptions> _options;

        public DefaultDeviceFactory(IOptions<DeviceOptions> options)
        {
            _options = options;
        }

        public IDevice Normal() => new LiteDevice(DeviceType.Normal, string.Empty);
        public IDevice Mobile() => new LiteDevice(DeviceType.Mobile, _options.Value.MobileCode);
        public IDevice Tablet() => new LiteDevice(DeviceType.Tablet, _options.Value.TabletCode);
        public IDevice Other(string code) => new LiteDevice(DeviceType.Other, code);
    }
}