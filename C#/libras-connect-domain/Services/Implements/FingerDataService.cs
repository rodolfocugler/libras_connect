using libras_connect_domain.Models;
using libras_connect_domain.Services.Interfaces;
using System.Collections.Generic;

namespace libras_connect_domain.Services.Implements
{
    /// <summary>
    /// Implementation of IFingerDataService <see cref="IFingerDataService"/>
    /// </summary>
    public class FingerDataService : BaseRealsesenseService, IFingerDataService
    {
        /// <summary>
        ///     <para><see cref="IFingerDataService.OnProcessFinger(PXCMHandData.IHand)"/></para>
        /// </summary>
        public ICollection<FingerData> OnProcessFinger(PXCMHandData.IHand ihand)
        {
            List<FingerData> fingerDataRealsense = new List<FingerData>();

            for (int i = 0; i < 5; i++)
            {
                PXCMHandData.FingerData fingerData = null;
                base.CheckError(ihand.QueryFingerData((PXCMHandData.FingerType) i, out fingerData));
                fingerDataRealsense.Add(new FingerData(fingerData, i));
            }

            return fingerDataRealsense;
        }
    }
}
