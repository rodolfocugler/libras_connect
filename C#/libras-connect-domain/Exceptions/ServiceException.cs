using System;

namespace libras_connect_domain.Exceptions
{
    public class ServiceException : Exception
    {
        /// <summary>
        /// Exception of service layer 
        /// </summary>
        /// <param name="message">Message of exception</param>
        public ServiceException(String message) : base(message) { }

        /// <summary>
        /// Exception of service layer 
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="inner">Exception encapsulated</param>
        public ServiceException(String message, Exception inner) : base(message, inner) { }
    }
}
