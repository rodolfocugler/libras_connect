using libras_connect_domain.Enums;
using libras_connect_domain.Models;
using libras_connect_domain.Services.Interfaces;
using System.Collections.Generic;

namespace libras_connect_domain.Services.Implements
{
    /// <summary>
    /// Implementation of IJointDataService <see cref="IJointDataService"/>
    /// </summary>
    public class JointDataService : BaseRealsesenseService, IJointDataService
    {
        /// <summary>
        ///     <para><see cref="IJointDataService.OnProcessJoint(PXCMHandData.IHand)"/></para>
        /// </summary>
        public IDictionary<JointEnum, JointData> OnProcessJoint(PXCMHandData.IHand ihand)
        {
            IDictionary<JointEnum, JointData> jointDataRealsense = new Dictionary<JointEnum, JointData>();

            for (int i = 0; i < 22; i++)
            {
                PXCMHandData.JointData jointData = null;
                base.CheckError(ihand.QueryTrackedJoint((PXCMHandData.JointType)i, out jointData));
                JointData jd = new JointData(jointData, i);
                jointDataRealsense.Add(jd.JointEnum, jd);
            }

            return jointDataRealsense;
        }
    }
}
