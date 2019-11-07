using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.CustomCSS.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.CustomCSS.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    [AdminAntiForgery]
    public class WidgetsCustomCSSController : BasePluginController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        #endregion

        #region Ctor

        public WidgetsCustomCSSController(
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        #endregion

        #region Methods
        
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var CustomCSSSettings = _settingService.LoadSetting<CustomCSSSettings>(storeScope);

            var model = new ConfigurationModel
            {
                CSS = CustomCSSSettings.CSS,
                ActiveStoreScopeConfiguration = storeScope
            };

            if (storeScope > 0)
            {

                model.CSS_OverrideForStore = _settingService.SettingExists(CustomCSSSettings, x => x.CSS, storeScope);

            }

            return View("~/Plugins/Widgets.CustomCSS/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var customCSSSettings = _settingService.LoadSetting<CustomCSSSettings>(storeScope);

            customCSSSettings.CSS = model.CSS;


            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            _settingService.SaveSettingOverridablePerStore(customCSSSettings, x => x.CSS, model.CSS_OverrideForStore, storeScope, false);

            //now clear settings cache
            _settingService.ClearCache();

            return Configure();
        }

        #endregion
    }
}