namespace MvcDeviceDetector.Abstractions
{
    public interface IDeviceFactory
    {
        IDevice Normal();
        IDevice Mobile();
        IDevice Tablet();
        IDevice Other(string code);
    }
}