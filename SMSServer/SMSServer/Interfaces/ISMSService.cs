using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SMSServer.Interfaces
{
    public interface ISMSService
    {
        bool SendSMS(int intId, string strPhoneNumber, string strText);
        void RegisterReceiver();
        void UnregisterReceiver();
    }
}
