using System.Collections.Generic;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.CustomCSS
{
    /// <summary>
    /// Google Analytics plugin
    /// </summary>
    public class CustomCSSPlugin : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;

        #endregion

        #region Ctor

        public CustomCSSPlugin(ILocalizationService localizationService,
            IWebHelper webHelper,
            ISettingService settingService)
        {
            _localizationService = localizationService;
            _webHelper = webHelper;
            _settingService = settingService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { PublicWidgetZones.HeadHtmlTag };
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsCustomCSS/Configure";
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsCustomCSS";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            var settings = new CustomCSSSettings
            {
                CSS = @"/ *Enter Your CSS Here */",
            };
            _settingService.SaveSetting(settings);


            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CustomCSS.CSS", "CSS");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CustomCSS.CSS.Hint", "Enter your CSS here.");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<CustomCSSSettings>();

            //locales
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.CustomCSS.CSS");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.CustomCSS.CSS.Hint");


            base.Uninstall();
        }

        #endregion

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;
    }
}