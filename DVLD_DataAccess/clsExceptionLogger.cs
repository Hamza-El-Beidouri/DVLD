using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class clsExceptionLogger
    {

        public static void LogException(Exception e)
        {
            string SourceName = "DVLD";

            if (!EventLog.SourceExists(SourceName))
                EventLog.CreateEventSource(SourceName, "Application");

            EventLog.WriteEntry(SourceName, e.Message, EventLogEntryType.Error);

        }

    }
}
