using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Widgets.HomePageNewProducts.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;
using System.Linq;

namespace Nop.Plugin.Widgets.HomePageNewProducts.Components
{
    [ViewComponent(Name = "WidgetsHomePageNewProducts")]
    public class WidgetsHomePageNewProductsViewComponent : NopViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IProductService _productService;
        private readonly IProductModelFactory _productModelFactory;

        public WidgetsHomePageNewProductsViewComponent(ISettingService settingService, 
            IStoreContext storeContext,
            IProductService productService,
            IProductModelFactory productModelFactory)
        {
            this._settingService = settingService;
            this._storeContext = storeContext;
            this._productService = productService;
            this._productModelFactory = productModelFactory;
        }

        public IViewComponentResult Invoke()
        {
            var homePageNewProductsSettings = _settingService.LoadSetting<HomePageNewProductsSettings>(_storeContext.CurrentStore.Id);

            var products = (IPagedList<Product>)_productService.SearchProducts(
                storeId: _storeContext.CurrentStore.Id,
                markedAsNewOnly: true,
                orderBy: ProductSortingEnum.CreatedOn,
                pageSize: homePageNewProductsSettings.ProductsCount)
                    .Take(homePageNewProductsSettings.ProductsCount);

            var productOverviewModels = _productModelFactory.PrepareProductOverviewModels(products, true, true).ToList();

            var model = new PublicInfoModel
            {
                Products = productOverviewModels
            };
            
            return View("~/Plugins/Widgets.HomePageNewProducts/Views/PublicInfo.cshtml", model);
        }
    }
}
