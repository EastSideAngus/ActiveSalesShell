using ActiveSalesShell.Helpers;
using ActiveSalesShell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using JosePCL;
using System.Collections.ObjectModel;

namespace ActiveSalesShell.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        string _emailField = "joe.savoie@gmail.com";
        string _passwordField = "*******"; 

        public ICommand SignInCommand { get; }


        public string EmailField
        {
            get { return _emailField; }
            set { _emailField = value; }
        }

        public string PasswordField
        {
            get { return _passwordField; }
            set { _passwordField = value; }
        }

        public LoginViewModel()
        {
            SignInCommand = new Command(async () => await SignIn());
        }


        async Task SignIn()
        {
            // Attempt login here:
            var loggedin = await App.RestService.TryLoginAsync(_emailField, _passwordField);
            
            // Navigate to the Report (if we have an internet connection, and are logged in)
            if (loggedin)
            {
                // 1) GET CLUB AND USER ID for obj, and assign it to the ActiveUser
                var setup_the_user = await App.RestService.GetMostRecentClub(); // returns BOOL, sets the club id and user id in REST Service object

                // Navigate to main page, provide hamburger-menu and link to report page via the Navigation() object
                if (setup_the_user)
                {
                    App.isLoggedIn = true;
                    App.Current.MainPage = new Views.Navigation();

                    var bGetSalesReport = await App.RestService.GetActiveSalesReport();
                    if (bGetSalesReport)
                    {
                        // Successfully loaded the Sales Report Data!
                        // Do something else now...?
                        
                    }
                    
                }
                // ELSE: Display error!


                
                // NOTE:
                //App.Current.MainPage = new Views.ActiveSalesReportPage(); //<-- Goes directly to the report, does not initialize the menu
            }
            else
            {
                App.isLoggedIn = false;
                App.Current.MainPage = new Views.NoNetworkPage();
            }
            
        }


    }
}
