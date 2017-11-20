﻿using ActiveSalesShell.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActiveSalesShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActiveSalesReportPage : ContentPage
    {
        public ActiveSalesReportPage()
        {
            InitializeComponent();
            BindingContext = new ActiveSalesReportViewModel();

        }
    }
}