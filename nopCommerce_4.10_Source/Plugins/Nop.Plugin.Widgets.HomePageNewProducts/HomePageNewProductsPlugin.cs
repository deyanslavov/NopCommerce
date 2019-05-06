using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.HomePageNewProducts
{
    public class HomePageNewProductsPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public HomePageNewProductsPlugin(ILocalizationService localizationService, 
            ISettingService settingService, 
            IWebHelper webHelper)
        {
            this._localizationService = localizationService;
            this._settingService = settingService;
            this._webHelper = webHelper;
        }
        
        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsHomePageNewProducts";
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { PublicWidgetZones.HomePageTop };
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsHomePageNewProducts/Configure";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            var settings = new HomePageNewProductsSettings
            {
                ProductsCount = 4,  
            };

            _settingService.SaveSetting(settings);

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.ProductsCount", "Number of products to be shown.");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            _settingService.DeleteSetting<HomePageNewProductsSettings>();
            
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.ProductsCount");

            base.Uninstall();
        }
    }
}
