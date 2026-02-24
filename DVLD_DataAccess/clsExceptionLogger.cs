using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class clsExceptionLogger
    {

        public static void LogException(Exception ex)
        {
            
            string sourceName = "DVLD";

            if (!EventLog.SourceExists(sourceName))
                EventLog.CreateEventSource(sourceName, "Application");

            EventLog.WriteEntry(sourceName, ex.Message, EventLogEntryType.Error);

        }

    }
}
