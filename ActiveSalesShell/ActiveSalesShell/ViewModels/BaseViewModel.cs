using ActiveSalesShell.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveSalesShell.ViewModels
{
    public class BaseViewModel : ObservableObject
    {

        string message = string.Empty;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                if (value == false)
                {
                    //  NOT BUSY
                }
                OnPropertyChanged();
            }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set
            {
                SetProperty(ref title, value);
            }
        }

        public void DisplayAlert(string title, string message)
        {
            string[] values = { title, message };
            // MessagingCenter.Send<BaseViewModel, string[]>(this, "DisplayAlert", values);
        }
    }
}
