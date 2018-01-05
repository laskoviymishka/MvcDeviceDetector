namespace MvcSandbox.Controllers
{
    #region usings

    using Microsoft.AspNetCore.Mvc;
    using MvcDeviceDetector.Abstractions;

    #endregion

    public class HomeController : Controller
    {
        private readonly IDeviceAccessor _device;

        public HomeController(IDeviceAccessor device)
        {
            _device = device;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}