using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using ActiveSalesShell.Views;
using ActiveSalesShell.Helpers;
using ActiveSalesShell.Models;
using Plugin.Connectivity;
using System.Collections.ObjectModel;

namespace ActiveSalesShell
{
    public partial class App : Application
    {
        public static bool isLoggedIn = false;
        public static ObservableCollection<Sku> Skus_from_API { get; set; }

        public static UserModel ActiveUser = new UserModel();

        // SETUP the REST Service
        static RESTService _restService;
        public static RESTService RestService
        {
            get
            {
                if (_restService == null)
                {
                    _restService = new RESTService();
                }
                return _restService;
            }
        }

        public App()
        {
            InitializeComponent();
            
            // MainPage determined by where we have internet 
            MainPage = DoIHaveInternet() ? (Page)new LoginPage() : (Page)new NoNetworkPage();


            // TEST STUFF
            // MainPage = DoIHaveInternet() ? (Page)new NetworkViewPage() : (Page)new NoNetworkPage();
            // SetMainPage();
            // MainPage is the Navigation with the LoginPage 
            // MainPage = new NavigationPage((Page)new LoginPage());

        }


        // Check for internets using CrossConnectivity
        public bool DoIHaveInternet()
        {
            // https://jamesmontemagno.github.io/ConnectivityPlugin/GettingStarted.html
            if (!CrossConnectivity.IsSupported)
                return true;

            return CrossConnectivity.Current.IsConnected;
        }


        // NOT IN USE
        // Nic's Internet Connection checker --> uses Reachability.Net package
        public bool isConnectedToInternet()
        {
            var isConnected = true;
            /*
            #if __ANDROID__
			    var reachability = new Reachability.Net.XamarinAndroid.Reachability();
            #else
                var reachability = new Reachability.Net.XamarinIOS.Reachability();
            #endif
            
            var isConnected = reachability.IsHostReachable("https://api.powerupsports.com");
                */
            return (isConnected);
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static void SetMainPage()
        {
            if (!isLoggedIn)
            {
                LoginPage logInPage = (new LoginPage());
                Current.MainPage = new LoginPage();

                // TEST STUFF
                // Current.MainPage = new NoNetworkPage();
                // Current.MainPage = CrossConnectivity.Current.IsConnected ? (Page)new NetworkViewPage() : new NoNetworkPage();
            }
            else
            {
                // TODO: Update to send to an error page if unable to log in
                Current.MainPage = new Views.Navigation();
            }
        }
    }
}
