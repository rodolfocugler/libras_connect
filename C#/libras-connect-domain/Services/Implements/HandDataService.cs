using libras_connect_domain.Services.Interfaces;
using System.Collections.Generic;
using System;
using libras_connect_domain.Models;

namespace libras_connect_domain.Services.Implements
{
    /// <summary>
    /// Implementation of IHandDataService <see cref="IHandDataService"/>
    /// </summary>
    public class HandDataService : BaseRealsesenseService, IHandDataService
    {
        private readonly IJointDataService _jointDataService;
        private readonly IFingerDataService _fingerDataService;

        public HandDataService(IJointDataService jointDataService, IFingerDataService fingerDataService)
        {
            _fingerDataService = fingerDataService;
            _jointDataService = jointDataService;
        }

        /// <summary>
        ///     <para><see cref="IHandDataService.OnProcess(PXCMHandData)"/></para>
        /// </summary>
        public ICollection<HandData> OnProcess(PXCMHandData handData)
        {
            ICollection<HandData> handsData = new List<HandData>();

            try
            {
                handsData.Add(this.OnProcessHand(handData, PXCMHandData.AccessOrderType.ACCESS_ORDER_RIGHT_HANDS));
            }
            catch (Exception)
            {
            }

            try
            {
                handsData.Add(this.OnProcessHand(handData, PXCMHandData.AccessOrderType.ACCESS_ORDER_LEFT_HANDS));
            }
            catch (Exception)
            {
            }
            
            return handsData;
        }

        /// <summary>
        /// Process Hand
        /// </summary>
        /// <param name="handData">PXCMHandData</param>
        /// <param name="handEnum">PXCMHandData.AccessOrderType</param>
        /// <returns>HandData Model</returns>
        private HandData OnProcessHand(PXCMHandData handData, PXCMHandData.AccessOrderType handEnum)
        {
            PXCMHandData.IHand ihand = null;
            base.CheckError(handData.QueryHandData(handEnum, 0, out ihand));
            HandData handDataRealsense = null;

            if (ihand != null)
            {
                handDataRealsense = new HandData(ihand, (int)handEnum);

                if (ihand.HasTrackedJoints())
                {
                    handDataRealsense.FingerDatas = _fingerDataService.OnProcessFinger(ihand);
                    handDataRealsense.JointDatas = _jointDataService.OnProcessJoint(ihand);
                }
            }           

            return handDataRealsense;
        }
    }
}
