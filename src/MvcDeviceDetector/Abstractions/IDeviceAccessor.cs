namespace MvcDeviceDetector.Abstractions
{
    public interface IDeviceAccessor
    {
        IDevice Device { get; }
        IDevice Preference { get; }
    }
}