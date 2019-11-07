using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.CustomCSS
{
    public class CustomCSSSettings : ISettings
    {
        public string CSS { get; set; }
        public string WidgetZone { get; set; }

    }
}