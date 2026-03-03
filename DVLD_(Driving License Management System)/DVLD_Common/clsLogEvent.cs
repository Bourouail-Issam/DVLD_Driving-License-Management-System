using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Common
{
    public  class clsLogEvent
    {
        public static void LogEvent(string message, EventLogEntryType type)
        {
            const string sourceName = "DVLD_Application";
            try
            {
                EventLog.WriteEntry(sourceName, message, type);
            }
            catch (Exception ex)
            {
                try
                {
                    System.IO.File.AppendAllText(
                        "DVLD_log_backup.txt",
                        $"[{DateTime.Now}] [{type}]\n" +
                        $"Message: {message}\n" +
                        $"Log Error: {ex.Message}\n" +
                        $"------------------------\n");
                }
                catch { }
            }
        }
        public static bool CreateEventLog()
        {
            const string sourceName = "DVLD_Application";
            const string logName = "Application";

            try
            {
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, logName);
                }
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    System.IO.File.AppendAllText(
                        "critical_error.txt",
                        $"{DateTime.Now}: {ex.Message}\n");
                }
                catch { } 

                return false;
            }
        }
    }
}
