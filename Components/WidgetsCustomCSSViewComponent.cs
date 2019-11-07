using System;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Directory;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.CustomCSS.Components
{
    [ViewComponent(Name = "WidgetsCustomCSS")]
    public class WidgetsCustomCSSViewComponent : NopViewComponent
    {
        #region Fields


        private readonly CustomCSSSettings _customCSSSettings;
        private readonly ILogger _logger;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public WidgetsCustomCSSViewComponent(CustomCSSSettings customCSSSettings,
            ISettingService settingService,
            IStoreContext storeContext,
            IWorkContext workContext)
        {
            _customCSSSettings = customCSSSettings;
            _settingService = settingService;
            _storeContext = storeContext;
            _workContext = workContext;
        }

        #endregion

        #region Utilities

        private string FixIllegalJavaScriptChars(string text)
        {
            if (String.IsNullOrEmpty(text))
                return text;

            //replace ' with \' (http://stackoverflow.com/questions/4292761/need-to-url-encode-labels-when-tracking-events-with-google-analytics)
            text = text.Replace("'", "\\'");
            return text;
        }


        #endregion

        #region Methods

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            string style = _customCSSSettings.CSS;
            var routeData = Url.ActionContext.RouteData;

            try
            {
                var controller = routeData.Values["controller"];
                var action = routeData.Values["action"];

                if (controller == null || action == null)
                    return Content("");

            }
            catch (Exception ex)
            {
                _logger.InsertLog(Core.Domain.Logging.LogLevel.Error, "Error applying styles", ex.ToString());
            }
            return View("~/Plugins/Widgets.CustomCSS/Views/PublicInfo.cshtml", style);
        }

        #endregion
    }
}