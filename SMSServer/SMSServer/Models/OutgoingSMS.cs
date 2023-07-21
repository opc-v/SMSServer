using SMSExchange;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Text.Json;
using SMSServer.IP;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SMSServer.Models
{
    public class OutgoingSMS : INotifyPropertyChanged
    {
        private static int _NextId;

        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
        public string ClientCode { get; set; }
        public string OriginalId { get; set; }
        public DateTime OriginalDT { get; set; }
        public string IpEndpoint { get; set; }

        private DateTime? _SentDT;
        public DateTime? SentDT { 
            get 
            {
                return _SentDT; 
            } 
            set { SetProperty(ref _SentDT, value); }
        }
        private DateTime? _DeliveredDT;
        public DateTime? DeliveredDT
        {
            get { return _DeliveredDT; }
            set { SetProperty(ref _DeliveredDT, value); }
        }


        public OutgoingSMS()
        {
            Id = Interlocked.Increment(ref _NextId);
        }
        public OutgoingSMS(SMS4Json src)
        {
            //this.IsOutcoming = true;
            Id = Interlocked.Increment(ref _NextId);
            this.PhoneNumber = src.PhoneNumber;
            this.Text = src.Text;
            this.OriginalId = src.OriginalId;
            this.OriginalDT = src.OriginalDT;
            this.ClientCode = src.ClientCode;
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
    public enum ResponseKind
    {
        Sent = 0,
        Delivered = 1
    }
}
