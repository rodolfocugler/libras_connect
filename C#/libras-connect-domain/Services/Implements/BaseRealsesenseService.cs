using System;

namespace libras_connect_domain.Services.Implements
{
    public class BaseRealsesenseService
    {
        /// <summary>
        /// Check if status IsError
        /// </summary>
        /// <param name="status">pxcmStatus</param>
        protected void CheckError(pxcmStatus status)
        {
            if (status.IsError())
            {
                throw new Exception(System.Enum.GetName(typeof(pxcmStatus), status));
            }
        }
    }
}
