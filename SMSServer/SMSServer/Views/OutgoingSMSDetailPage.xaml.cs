using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SMSServer.Models;


namespace SMSServer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Id), "Id")]
    public partial class OutgoingSMSDetailPage : ContentPage
    {
        public new int Id
        {
            set
            {
                LoadOutgoingSMS(value);
            }
        }
        public OutgoingSMSDetailPage()
        {
            InitializeComponent();
        }

        void LoadOutgoingSMS(int intId)
        {
            try
            {
                OutgoingSMS sms = App.SMSVM.Items.FirstOrDefault(a => a.Id == intId);
                BindingContext = sms;
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, "Failed to load Outgoing SMS.");
            }
        }
    }


 
}