using ActiveSalesShell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActiveSalesShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationPageMaster : ContentPage
    {
        public ListView ListView;

        public NavigationPageMaster()
        {
            InitializeComponent();

            BindingContext = new NavigationPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class NavigationPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<NavigationPageMenuItem> MenuItems { get; set; }

            public NavigationPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<NavigationPageMenuItem>(new[]
                {
                    // new NavigationPageMenuItem { Id = 0, Title = "Login", TargetType = typeof(LoginPage)  },
                    new NavigationPageMenuItem { Id = 0, Title = "Active Sales Report", TargetType = typeof(ActiveSalesReportPage)  },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}