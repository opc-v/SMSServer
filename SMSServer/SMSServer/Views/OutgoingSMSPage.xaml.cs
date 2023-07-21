using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SMSServer.Models;
using SMSServer.ViewModels;

namespace SMSServer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OutgoingSMSPage : ContentPage
	{
		public OutgoingSMSPage ()
		{
			InitializeComponent ();
            BindingContext = App.SMSVM;
        }

        async private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int intId = (e.CurrentSelection.FirstOrDefault() as OutgoingSMS).Id;
            // The following route works because route names are unique in this application.
            await Shell.Current.GoToAsync($"outgoingsmsdetails?Id={intId}");
            //await Task.Delay (1000);
            //this.ForceLayout();
        }
    }
}