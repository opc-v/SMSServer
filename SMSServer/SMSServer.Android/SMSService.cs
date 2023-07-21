using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Telephony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using SMSServer.Interfaces;


[assembly: Dependency(typeof(SMSServer.Droid.SMSService))]
namespace SMSServer.Droid
{
    public class SMSService : Interfaces.ISMSService
    {
        SMSSentReceiver _SMSSentReceiver;
        SMSDeliveredReceiver _SMSDeliveredReceiver;
        static readonly int SEND_SMS_CODE = 1341;
        public const String SMS_SENT = "SMS_SENT";
        public const String SMS_DELIVERED = "SMS_DELIVERED";
        public const String SMSId = "com.companyname.smsserver.SMSId";

        public SMSService()
        {
            _SMSSentReceiver = new SMSSentReceiver();
            _SMSDeliveredReceiver = new SMSDeliveredReceiver();
        }

        bool ISMSService.SendSMS(int intSMSId, string strPhoneNumber, string strText)
        {
            Intent sentIntent = new Intent(SMS_SENT);
            sentIntent.PutExtra(SMSId, intSMSId);  //
            //sentIntent.SetIdentifier(intSMSId.ToString());
 
            //PendingIntent.GetForegroundService
            PendingIntent piSent = PendingIntent.GetBroadcast(MainActivity.Instance.ApplicationContext, intSMSId, sentIntent,  PendingIntentFlags.Immutable);  // PendingIntentFlags.OneShot +
           
            Intent deliveredIntent = new Intent(SMS_DELIVERED);
            deliveredIntent.PutExtra(SMSId, intSMSId);
            PendingIntent piDelivered = PendingIntent.GetBroadcast(MainActivity.Instance.ApplicationContext, intSMSId, deliveredIntent, PendingIntentFlags.Immutable);  //Android.App.Application.Context
            //Xamarin.Forms.Application
            SmsManager manager = SmsManager.Default;

            SmsManager.Default.SendTextMessage(strPhoneNumber, null, strText, piSent, piDelivered);
            //var msgs = SmsManager.Default.DivideMessage(strText);
            //SmsManager.Default.SendMultipartTextMessage(strPhoneNumber, null, msgs, piSent, piDelivered);
            return true;
        }

        void ISMSService.RegisterReceiver()
        {
            MainActivity.Instance.ApplicationContext.RegisterReceiver(_SMSSentReceiver, new IntentFilter(SMS_SENT));
            MainActivity.Instance.ApplicationContext.RegisterReceiver(_SMSDeliveredReceiver, new IntentFilter(SMS_DELIVERED));
        }

        void ISMSService.UnregisterReceiver()
        {
            MainActivity.Instance.ApplicationContext.UnregisterReceiver(_SMSSentReceiver);
            MainActivity.Instance.ApplicationContext.UnregisterReceiver(_SMSDeliveredReceiver);
        }
    }
    #region BroadcastReceivers
    [BroadcastReceiver]
    public class SMSSentReceiver : BroadcastReceiver
    {
        public const String SMSId = "com.companyname.smsserver.SMSId";
        public override void OnReceive(Context context, Intent intent)
        {

            int intSMSId =  intent.GetIntExtra(SMSId, -1);
             switch ((int)this.ResultCode)
            {
                case (int)Result.Ok:
                    App.SMSVM.SMSResponseProcessor(intSMSId, SMSServer.Models.ResponseKind.Sent, null);
                    break;
                default:
                    App.SMSVM.SMSResponseProcessor(intSMSId, SMSServer.Models.ResponseKind.Sent, this.ResultCode.ToString());
                    break;
            }
        }
    }

    [BroadcastReceiver]
    public class SMSDeliveredReceiver : BroadcastReceiver
    {
        public const String SMSId = "com.companyname.smsserver.SMSId";
        public override void OnReceive(Context context, Intent intent)
        {
            int intSMSId = 0;
            intSMSId = intent.GetIntExtra(SMSId, - 1);
            switch ((int)ResultCode)
            {
                case (int)Result.Ok:
                    App.SMSVM.SMSResponseProcessor(intSMSId, SMSServer.Models.ResponseKind.Delivered, null);
                    break;
                default:
                    App.SMSVM.SMSResponseProcessor(intSMSId, SMSServer.Models.ResponseKind.Delivered, this.ResultCode.ToString());
                    break;
            }
        }
    }
    #endregion
}