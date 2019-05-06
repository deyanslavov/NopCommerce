using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.HomePageNewProducts.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.HomePageNewProducts.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsHomePageNewProductsController : BasePluginController
    {
        private readonly IPermissionService _permissionService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;

        public WidgetsHomePageNewProductsController(IPermissionService permissionService, 
            IStoreContext storeContext, 
            ISettingService settingService,
            ILocalizationService localizationService)
        {
            this._permissionService = permissionService;
            this._storeContext = storeContext;
            this._settingService = settingService;
            this._localizationService = localizationService;
        }

        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var homePageNewProductsSettings = _settingService.LoadSetting<HomePageNewProductsSettings>(storeScope);

            var model = new ConfigurationModel
            {
                ProductsCount = homePageNewProductsSettings.ProductsCount
            };

            if (storeScope > 0)
            {
                model.ProductsCount_OverrideForStore = _settingService.SettingExists(homePageNewProductsSettings, x => x.ProductsCount, storeScope);
            }

            return View("~/Plugins/Widgets.HomePageNewProducts/Views/Configure.cshtml", model);
        }

        [Area(AreaNames.Admin)]
        [HttpPost, ActionName("Configure")]
        public IActionResult Configure(ConfigurationModel configurationModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var homePageNewProductsSettings = _settingService.LoadSetting<HomePageNewProductsSettings>(storeScope);

            homePageNewProductsSettings.ProductsCount = configurationModel.ProductsCount;

            _settingService.SaveSettingOverridablePerStore(homePageNewProductsSettings, x => x.ProductsCount, configurationModel.ProductsCount_OverrideForStore, storeScope, false);

            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}
