using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveSalesShell.Models
{
    public class Sku
    {
        public Sku() { }

        public int Id { get; set; } // sku_id 
        public string Name { get; set; } // sku_name 
        public int Status { get; set; } // sku_status 
        public int QuantityRemaining { get; set; } // current_inventory 
        public int QuantitySold { get; set; } // quantity_sold
        public int WailtList { get; set; } // wait_list
        public int POA { get; set; } // poa

        public int ProductId { get; set; } // product_id
        public string ProductName { get; set; } // product_name
        public string ProductDescription { get; set; } // product_description
        public string SkuDescription { get; set; }  // sku_description
       

        public string NameSort => Name[0].ToString();

        /*
            public int ClubId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public int Price { get; set; }
            public int BackOrder { get; set; }
            public int AutomaticPayment { get; set; }
            public int Status { get; set; }
            public int TaxReceipt { get; set; }
            public int SkuRestrictionId { get; set; }

            public int ProductRegRequired { get; set; }
            public int LanguageId { get; set; }

            public string Code { get; set; }
            public string RegistrarProgram { get; set; }
            public string RegistrarGroup { get; set; }
            public string RegistrarSubrole { get; set; }
        
            public string Url { get; set; }
            public string Description { get; set; }
            public string TaxReceiptDescription { get; set; }
            public string UnlockCode { get; set; }

            public DateTime PaymentDueDate { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime DateModified { get; set; }
        */

    }

    public class GroupedSkusByProduct<K, Sku> : ObservableCollection<Sku>
    {
        public K Key { get; private set; }

        public GroupedSkusByProduct(K key, IEnumerable<Sku> items)
        {
            Key = key;
            foreach (var item in items) this.Items.Add(item);
        }

    }


}
