using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SMSServer.ViewModels;

namespace SMSServer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationPage : ContentPage
    {
        public InformationPage()
        {
            InitializeComponent();
            BindingContext = App.SMSVM;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //BindingContext = App.SMSVM;
            //InformationViewModel src = new InformationViewModel();
            //BindingContext = src;
        }
    }
}