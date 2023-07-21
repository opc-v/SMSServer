using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Text.Json;

namespace SMSExchange
{
    public class SMS4Json
    {
        private static int _NextId;
        public SMS4Json()
        {
            OriginalId = Interlocked.Increment(ref _NextId).ToString();
            OriginalDT = DateTime.UtcNow;
        }
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
        public string ClientCode { get; set; }
        public string OriginalId { get; set; }
        public DateTime OriginalDT { get; set; }
    }

    public class ListOfSMS : List<SMS4Json>
    {
    }

    public class TransportUnit
    {
        public string IpEndpoint { get; set; }
        private static int _Count;
        public int Id { get; set; }

        public ListOfSMS SMSList { get; set; }

        public void AddSMS(SMS4Json sms)
        {
            if (sms == null) throw new ArgumentNullException("sms");
            if (SMSList == null) SMSList = new ListOfSMS();
            SMSList.Add(sms);
            return;
        }
        public void Clear()
        {
            SMSList = null;
        }
        public byte[] GetJsonBytes()
        {
            this.Id = Interlocked.Increment(ref _Count);

            byte[] bytes = JsonSerializer.SerializeToUtf8Bytes<TransportUnit>(this);
            byte[] length = BitConverter.GetBytes((UInt16)bytes.Length);

            Array.Resize(ref length, bytes.Length + 2);

            Array.Copy(bytes, 0, length, 2, bytes.Length);

            return length;
        }
        public static TransportUnit DeSerialize(ReadOnlySpan<byte> rosSource)
        {
            return JsonSerializer.Deserialize<TransportUnit>(rosSource);
        }
    }

    public class Response
    {
        public string SMSOriginalId { get; set; }
        public byte Kind { get; set; } //0-udefuned, 1-text, 2-exception
        public string Text { get; set; }
    }
    public class ListOfResponse : List<Response>
    {
    }
    public class ResponseTransport
    {
        public object Token;
        private static int _Count;
        public int Id { get; set; }

        public ListOfResponse ResponseList { get; set; }

        public void AddResponse(Response rsp)
        {
            if (rsp == null) throw new ArgumentNullException("rsp");
            if (ResponseList == null) ResponseList = new ListOfResponse();
            ResponseList.Add(rsp);
            return;
        }
        public void Clear()
        {
            ResponseList = null;
        }
        public byte[] GetJsonBytes()
        {
            Interlocked.Increment(ref _Count);
            this.Id = _Count;

            byte[] bytes = JsonSerializer.SerializeToUtf8Bytes< ResponseTransport>(this);

            byte[] length = BitConverter.GetBytes((UInt16)bytes.Length);

            Array.Resize(ref length, bytes.Length + 2);

            Array.Copy(bytes, 0, length, 2, bytes.Length);
            return length;
        }
        public static ResponseTransport DeSerialize(ReadOnlySpan<byte> rspSource)
        {
            return JsonSerializer.Deserialize<ResponseTransport>(rspSource);
        }
    }
}
