using Nop.Core.Configuration;
using System;

namespace Nop.Plugin.Widgets.HomePageNewProducts
{
    public class HomePageNewProductsSettings : ISettings
    {
        public int ProductsCount { get; set; } = 4;
        public bool ProductsCount_OverrideForStore { get; set; }
    }
}
