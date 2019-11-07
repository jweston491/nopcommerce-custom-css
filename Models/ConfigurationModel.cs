using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.CustomCSS.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }
       

        [NopResourceDisplayName("Plugins.Widgets.CustomCSS.CSS")]
        public string CSS { get; set; }
        public bool CSS_OverrideForStore { get; set; }
        
    }
}