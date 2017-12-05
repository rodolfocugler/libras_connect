using libras_connect_domain.Models;
using System.Collections.Generic;

namespace libras_connect_domain.Services.Interfaces
{
    public interface IHandDataService
    {
        /// <summary>
        /// Get HandData by HandEnum
        /// </summary>
        /// <param name="handData">PXCMHandData</param>
        /// <returns>HandData Collection</returns>
        ICollection<HandData> OnProcess(PXCMHandData handData);
    }
}
