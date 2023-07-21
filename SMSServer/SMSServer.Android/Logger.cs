using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Util;
using SMSServer.Interfaces;

using AndroidX.Core.App;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

[assembly: Dependency(typeof(SMSServer.Droid.Logger))]
namespace SMSServer.Droid
{
    internal class Logger : Interfaces.ILogService
    {
        private bool _IsUseDebugConsole = false;
        private string _LogTag;
        public string LogTag
        {
            get
            {
                return _LogTag;
            }
            set
            {
                _LogTag = value;
            }
        }

        public Logger() {
            _LogTag = "SMSServerLogTag";
#if DEBUG
            _IsUseDebugConsole = true;
#else
            _IsUseDebugConsole = false;
#endif
        }

        public enum LogMessageType
        {
            Info,
            Warn,
            Error,
            Exception,
            Fatal
        };

        public bool Debug(string msg)
        {
            if (_IsUseDebugConsole) DebugWriteLine(LogMessageType.Info, msg);
            else if (Log.Debug(_LogTag, msg) > 0) return true;
            return false;
        }

        public bool Error(string msg)
        {
            if (_IsUseDebugConsole) DebugWriteLine(LogMessageType.Error, msg);
            else if (Log.Error(_LogTag, msg) > 0) return true;
            return false;
        }

        public bool Fatal(string msg)
        {
            if (_IsUseDebugConsole) DebugWriteLine(LogMessageType.Fatal, msg);
            else if (Log.Wtf(_LogTag, msg) > 0) return true;
            return false;
        }

        public bool Info(string msg)
        {
            if (_IsUseDebugConsole) DebugWriteLine(LogMessageType.Info, msg);
            else if (Log.Info(_LogTag, msg) > 0) return true;
            return false;
        }

        public bool Warn(string msg)
        {
            if (_IsUseDebugConsole) DebugWriteLine(LogMessageType.Warn, msg);
            else if (Log.Warn(_LogTag, msg) > 0) return true;
            return false;
        }

        public bool Exception(Exception ex, string msg)
        {
            string strT = ex.Message + "\r\n" + ex.StackTrace;
            if(!string.IsNullOrEmpty(strT)) strT = msg +" " + strT ;
            if (_IsUseDebugConsole) DebugWriteLine( LogMessageType.Exception, strT);
            else if (Log.Error(_LogTag, strT) > 0) return true;
            return false;
        }
        private void DebugWriteLine(LogMessageType type, string msg)
        {
            string strHeader;
            strHeader = string.Concat(DateTime.Now.ToString("HH.mm.ss:fff"), type.ToString().PadLeft(10), Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(10), Task.CurrentId.ToString().PadLeft(10));
            System.Diagnostics.Debug.WriteLine(strHeader);
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }

}