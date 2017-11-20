using ActiveSalesShell.Helpers;
using ActiveSalesShell.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;                   // FOR DEBUG STUFF !!!
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Windows.Input;

namespace ActiveSalesShell.ViewModels
{
    public class ActiveSalesReportViewModel : BaseViewModel
    {
        
        public int id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        
        public int QuantitySold { get; set; }
        public int QuantityRemaining { get; set; }
        public int WailtList { get; set; }
        public string Name { get; set; }  // SkuName

        //private bool _isDataLoaded = false;
        
        //public static ObservableCollection<Sku> APISkus { get; set; }
        
        public ICommand LoadFromServer { get; }
        public ObservableCollection<Sku> Skus { get; set; }
        
        public ActiveSalesReportViewModel()
        {
            Skus = App.Skus_from_API;  // THIS DATA IS LOADED after login


            // Skus = SkuHelper.Skus;
            // LoadFromServer = new Command(async () => await GetReport());
            // LoadFromServer = new Command(async () => await App.RestService.GetActiveSalesReport());

        }


        // NOT USING THIS PRESENTLY  | Was used for testing
        public async Task GetReport()
        {
          
            try
            {
                // isBusy = true;
                //var skus = await App.RestService.GetActiveSalesReport();
                // THIS WORKS!
                // var skus = await App.RestService.GetResponse<Sku>("https://api.powerupsports.com/index.php?type=active_sales&club_id=2");
                // IF NOT NULL, DO SOMETHING ...
                // var config = await App.RestService.GetResponse<Sku>("https://api.powerupsports.com/index.php?type=config_all&club_id=" + App.ActiveUser.ClubId);
                
                var test = await App.RestService.GetResponse<Sku>("https://api.powerupsports.com/index.php?type=config_all&club_id=" + App.ActiveUser.ClubId);

                // Sku[] skus = await App.RestService.GetResponse<Sku>("https://api.powerupsports.com/index.php?type=active_sales&club_id=2");
                //Skus = new ObservableCollection<Sku>();

                /*
                foreach (Sku sku in skus)
                {
                    Skus.Add(new Sku
                    {
                        Id = sku->sku_id,
                        ProductId = sku->sku_id,
                        ProductName = sku->sku_name,
                        QuantitySold = sku->quantity_sold,
                        QuantityRemaining = sku->current_inventory,
                        POA = sku->poa,
                        WailtList = sku->wait_list,
                        Name = sku->sku_name
                    });
                }
                
                */

                //var skus = JsonConvert.DeserializeObject < IEnumerable < Sku >>(result);

                // _isDataLoaded = true;

                // foreach (Sku sku in skus)
                //   Skus.Add(sku);

            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                //throw;
            }
            
        }

    }
}