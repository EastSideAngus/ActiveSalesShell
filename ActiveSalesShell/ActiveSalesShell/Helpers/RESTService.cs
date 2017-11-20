using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
//using ModernHttpClient;
using ActiveSalesShell.Models;
using System.Collections.ObjectModel;

namespace ActiveSalesShell.Helpers
{
    public class RESTService
    {
        HttpClient client;
        public List<Sku> apiskus = new List<Sku>();
        public static ObservableCollection<Sku> SKUs { get; set; }

        private string _authtoken = string.Empty;
        public string authtoken
        {
            get { return (_authtoken); }
        }

        private string _userid = string.Empty;
        public string UserId
        {
            get { return (_userid); }
            set { _userid = value; }
        }

        private string _clubid = string.Empty;
        public string ClubId
        {
            get { return (_clubid); }
            set { _clubid = value; }
        }


        // CONSTRUCTOR
        public RESTService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        
        // Called via the LoginViewModel.SignIn() button click
        public async Task<bool> TryLoginAsync(string username, string password)
        {
            string url = "https://api.powerupsports.com";
            bool bReturn = false;

            // Test connection to *.powerupsports.com
            var result = await CrossConnectivity.Current.IsRemoteReachable(url);
            _authtoken = "";
            
            if (result)
            {
                url += "?user=" + System.Uri.EscapeDataString(username) + "&pass=" + System.Uri.EscapeDataString(password);
                var responseFromServer = await GetPowerUpURLAsync(url);
                if (responseFromServer.Length > 6)
                {
                    _authtoken = responseFromServer;
                    bReturn = true;
                }
            }
            return bReturn;
        }


        // Called on LOGIN to set the current user_id and club_id
        public async Task<bool> GetMostRecentClub()
        {
            // Get the response object.
            var url = "https://api.powerupsports.com?type=most_recent_club_by_user";

            var responseFromServer = await GetPowerUpURLAsync(url);
            if (responseFromServer.Length > 0)
            {

                JToken attributes = GetTheGoodStuff(responseFromServer);
                if (attributes != null)
                {
                    App.ActiveUser.Id = Int32.Parse(attributes[0]["user_id"].ToString());
                    App.ActiveUser.ClubId = Int32.Parse(attributes[0]["club_id"].ToString());
                    return (true);
                }
            }
            return (false);
        }


        // Generate the data (array of SKU objects) for use on the ActiveSalesReport page
        public async Task<bool> GetActiveSalesReport()
        {
            var url = "https://api.powerupsports.com?type=active_sales&club_id=" + App.ActiveUser.ClubId;
            var responseFromServer = await GetPowerUpURLAsync(url);

            if (responseFromServer.Length > 0)
            {
                JToken attributes = GetTheGoodStuff(responseFromServer);
                if (attributes != null)
                {
                    // Place data into a collection, for use on the report page
                    SKUs = new ObservableCollection<Sku>();
                    foreach (JToken token in attributes)
                    {
                        SKUs.Add(new Sku
                        {
                            Id = Int32.Parse(token.SelectToken("sku_id").ToString()),
                            Name = (token.SelectToken("sku_name").ToString()),
                            Status = Int32.Parse(token.SelectToken("sku_status").ToString()),
                            QuantityRemaining = Int32.Parse(token.SelectToken("current_inventory").ToString()),
                            QuantitySold = Int32.Parse(token.SelectToken("quantity_sold").ToString()),
                            WailtList = Int32.Parse(token.SelectToken("wait_list").ToString()),
                            POA = Int32.Parse(token.SelectToken("poa").ToString()),
                            ProductId = Int32.Parse(token.SelectToken("product_id").ToString()),
                            ProductName = (token.SelectToken("product_name").ToString()),
                            ProductDescription = (token.SelectToken("product_description").ToString()),
                            SkuDescription = (token.SelectToken("sku_description").ToString())
                        });
                    }
                    App.Skus_from_API = SKUs;
                    return (true);
                }
                // TODO: If no data, set something, like an error flag? (Maybe have an error object for holding any set errors?
            }
            return (false);
        }


        // Return a JToken containing the data from the main attributes sub-structure of the JSON string it receives
        private JToken GetTheGoodStuff(string response)
        {
            JToken attributes = null;
            try
            {
                JObject mostRecentResponse = JObject.Parse(response);
                attributes = mostRecentResponse.SelectToken("data[0].attributes");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }
            return (attributes);

        }

        
        // Return a string of JSON from an API call to the PowerUp API, checks and sets the AUTH token as well.
        private async Task<String> GetPowerUpURLAsync(String url)
        {
            using (HttpClient client = new HttpClient(new ModernHttpClient.NativeMessageHandler()))
            {
                string responseFromServer = "";

                // Check if already Authorized, if so, set the header
                if (_authtoken.Length > 0)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authtoken);
                }
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,* /*;q=0.8");
                // Get the response object
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    responseFromServer = await response.Content.ReadAsStringAsync();
                    if (_authtoken.Length == 0) _authtoken = responseFromServer;
                }
                catch (Exception e)
                {
                    return (e.ToString());
                }
                return (responseFromServer);
            }
        }




        // NOT USING THIS PRESENTLY  | Was used for testing
        // Return an object of type <T> (whatever is sent to the function) as a response from the API call. AUTH is set on the call
        public async Task<T> GetResponse<T>(string weburl) where T : class
        {
            //using (HttpClient client = new HttpClient(new ModernHttpClient.NativeMessageHandler()))
            //{
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authtoken);
                try
                {
                    var result = await client.GetAsync(weburl);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                       var jsonResult = result.Content.ReadAsStringAsync().Result;
                        try
                        {
                            var json_skus = GetTheGoodStuff(jsonResult); 
                            // RETURNS a JToken with the data from the attributes field of the JSON string (which is all we really want)
                            // var objs = JsonConvert.DeserializeObject<T>(json_skus.ToString());

                            var contentResp = JsonConvert.DeserializeObject<T>(jsonResult);
                            return contentResp;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
                catch
                {
                    return null;
                }
                return null;
            //}
        }

    }

}
