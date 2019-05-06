using Nop.Web.Models.Catalog;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.HomePageNewProducts.Models
{
    public class PublicInfoModel
    {
        public ICollection<ProductOverviewModel> Products { get; set; } = new List<ProductOverviewModel>();
    }
}
