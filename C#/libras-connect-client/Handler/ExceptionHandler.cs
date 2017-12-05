using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_client.Handler
{
    public class ExceptionHandler
    {
        private Exception _exception;
        
        public ExceptionHandler(Exception ex)
        {
            _exception = ex;

           /* using (EventLog eventLog = new EventLog("librascli"))
            {
                eventLog.Source = "librascli";
                eventLog.WriteEntry(_exception.Message, EventLogEntryType.Error);
            }*/
        }
    }
}
