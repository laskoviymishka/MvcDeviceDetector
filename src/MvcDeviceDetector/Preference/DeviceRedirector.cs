using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using MvcDeviceDetector.Abstractions;

namespace MvcDeviceDetector.Preference
{
    public class DeviceRedirector : IDeviceRedirector
    {
        private readonly IOptions<DeviceOptions> _options;
        private readonly IOptions<SwitcherOptions> _switcherOptions;

        public DeviceRedirector(IOptions<DeviceOptions> options, IOptions<SwitcherOptions> switcherOptions)
        {
            _options = options;
            _switcherOptions = switcherOptions;
        }

        public virtual void RedirectToDevice(HttpContext context, string code = "")
        {
            var referrerUrl = context.Request.GetDisplayUrl();
            if (context.Request.Headers.ContainsKey("Referer"))
            {
                referrerUrl = context.Request.Headers["Referer"];
            }

            context.Response.Redirect(DeviceUrl(ResetUrl(referrerUrl), code));
        }

        protected virtual string DeviceUrl(string resetUrl, string code)
            => resetUrl.Replace("//", string.IsNullOrWhiteSpace(code) ? "//" : $"//{code}.");

        protected virtual string ResetUrl(string referrerUrl)
            => referrerUrl
                .Replace($"/{_switcherOptions.Value.SwitchUrl}/{_switcherOptions.Value.NormalKey}", "")
                .Replace($"/{_switcherOptions.Value.SwitchUrl}/{_switcherOptions.Value.MobileKey}", "")
                .Replace($"/{_switcherOptions.Value.SwitchUrl}/{_switcherOptions.Value.TabletKey}", "")
                .Replace($"/{_switcherOptions.Value.SwitchUrl}/{_switcherOptions.Value.ResetKey}", "")
                .Replace($"//{_options.Value.TabletCode}.", "//")
                .Replace($"//{_options.Value.MobileCode}.", "//");
    }
}