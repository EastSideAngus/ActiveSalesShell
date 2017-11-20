using ActiveSalesShell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveSalesShell.Helpers
{
    public static class SkuHelper
    {
        public static ObservableCollection<Sku> Skus { get; set; }
        public static ObservableCollection<GroupedSkusByProduct<string, Sku>> GroupedSkus { get; set; }

        static SkuHelper()
        {

            Skus = new ObservableCollection<Sku>();
            Skus.Add(new Sku
            {
                Id = 1,
                ProductId = 1,
                ProductName = "Winter Camp",
                QuantitySold = 50,
                QuantityRemaining = 0,
                WailtList = 2,
                Name = "Sold Out Sku"
            });
            Skus.Add(new Sku
            {
                Id = 2,
                ProductId = 1,
                ProductName = "Winter Camp",
                QuantitySold = 480,
                QuantityRemaining = 20,
                WailtList = 0,
                Name = "Sku with available stuff"
            });
            Skus.Add(new Sku
            {
                Id = 3,
                ProductId = 1,
                ProductName = "Winter Camp",
                QuantitySold = 125,
                QuantityRemaining = 25,
                WailtList = 5,
                Name = "Another Sku"
            });
            Skus.Add(new Sku
            {
                Id = 4,
                ProductId = 2,
                ProductName = "Summer Camp",
                QuantitySold = 100,
                QuantityRemaining = 25,
                WailtList = 5,
                Name = "Another Sku, another Product"
            });
            Skus.Add(new Sku
            {
                Id = 5,
                ProductId = 2,
                ProductName = "Summer Camp",
                QuantitySold = 25,
                QuantityRemaining = 25,
                WailtList = 0,
                Name = "Another Sku for second product"
            });

            // Group by Product.Name, sort by Sku.Name
            var sorted = from sku in Skus
                         orderby sku.Name
                         group sku by sku.ProductName into SkuGroup
                         select new GroupedSkusByProduct<string, Sku>(SkuGroup.Key, SkuGroup);

            GroupedSkus = new ObservableCollection<GroupedSkusByProduct<string, Sku>>(sorted);
        }
    }
}
