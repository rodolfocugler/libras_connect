using System;

namespace libras_connect_domain.Exceptions
{
    public class RepositoryException : Exception
    {
        /// <summary>
        /// Exception of repository layer 
        /// </summary>
        /// <param name="message">Message of exception</param>
        public RepositoryException(String message) : base(message) { }

        /// <summary>
        /// Exception of repository layer 
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="inner">Exception encapsulated</param>
        public RepositoryException(String message, Exception inner) : base(message, inner) { }
    }
}
