﻿namespace MvcDeviceDetector.Abstractions
{
	using Microsoft.AspNetCore.Http;

	public interface IDeviceSwitcher
    {
        int Priority { get; }
        IDevice LoadPreference(HttpContext context);
        void StoreDevice(HttpContext context, IDevice device);
        void ResetStore(HttpContext context);
    }
}