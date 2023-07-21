using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SMSServer.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SMSServer.Data
{
    public static class OutgoingSMSData
    {
        public static ObservableCollection<OutgoingSMS> OutgoingSMSs //need for OutgoingSMSSearchHandler
        {
            get { return App.SMSVM.Items; }
        }
    }
}
