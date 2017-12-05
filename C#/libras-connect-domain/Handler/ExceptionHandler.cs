using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Handler
{
    public class ExceptionHandler
    {
        private Exception _exception;
        
        public ExceptionHandler(Exception ex)
        {
            _exception = ex;

            Console.WriteLine(ex.Message);

            /*using (EventLog eventLog = new EventLog("librascam"))
            {
                eventLog.Source = "librascam";
                eventLog.WriteEntry(_exception.Message, EventLogEntryType.Error);
            }*/
        }
    }
}
