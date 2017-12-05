using libras_connect_domain.Models;
using System.Collections.Generic;

namespace libras_connect_domain.Services.Interfaces
{
    public interface IFingerDataService
    {
        /// <summary>
        /// Get FingerData Collection
        /// </summary>
        /// <param name="ihand">PXCMHandData.IHand</param>
        /// <returns>FingerData ICollection</returns>
        ICollection<FingerData> OnProcessFinger(PXCMHandData.IHand ihand);
    }
}
