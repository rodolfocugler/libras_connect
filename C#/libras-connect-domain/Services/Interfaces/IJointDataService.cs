using libras_connect_domain.Enums;
using libras_connect_domain.Models;
using System.Collections.Generic;

namespace libras_connect_domain.Services.Interfaces
{
    public interface IJointDataService
    {
        /// <summary>
        /// Get JointData Collection
        /// </summary>
        /// <param name="ihand">PXCMHandData.IHand</param>
        /// <returns>JointData Dictionary [Key = JointEnum, Value = JointData]</returns>
        IDictionary<JointEnum, JointData> OnProcessJoint(PXCMHandData.IHand ihand);
    }
}
