using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.HomePageNewProducts.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName("Plugins.Widgets.HomePageNewProducts.ProductsCount")]
        public int ProductsCount { get; set; }
        public bool ProductsCount_OverrideForStore { get; set; }
    }
}
