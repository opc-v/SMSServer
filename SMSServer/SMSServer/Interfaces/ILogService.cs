using System;
using System.Collections.Generic;
using System.Text;

namespace SMSServer.Interfaces
{
    public interface ILogService
    {
        string LogTag { get; set; }

        bool Debug(string msg);
        bool Info(string msg);
        bool Warn(string msg);
        bool Error(string msg);
        bool Fatal(string msg);
        
        bool Exception(Exception ex,string msg);
    }
}
