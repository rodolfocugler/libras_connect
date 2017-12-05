using libras_connect_domain.DTO;
using libras_connect_domain.Handler;
using libras_connect_domain.Models;
using libras_connect_domain.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace libras_connect_domain.Services.Implements
{
    /// <summary>
    /// Implementation of IRealsenseService <see cref="IRealsenseService"/>
    /// </summary>
    public class RealsenseDataService : BaseRealsesenseService, IRealsenseService, IObserver
    {
        private static bool _isRunning;
        private readonly IHandDataService _handDataService;
        private readonly IDataService _dataService;
        private readonly IRealsenseAlertService _realsenseAlertService;

        public RealsenseDataService(IHandDataService handDataService, IDataService dataService, IRealsenseAlertService realsenseAlertService)
        {
            _isRunning = true;
            _handDataService = handDataService;
            _dataService = dataService;
            _realsenseAlertService = realsenseAlertService;
        }

        /// <summary>
        ///     <para><see cref="IRealsenseService.Start()"/></para>
        /// </summary>
        public void Start()
        {
            try
            {
                using (PXCMSession session = PXCMSession.CreateInstance())
                {
                    using (PXCMSenseManager sm = session.CreateSenseManager())
                    {
                        base.CheckError(sm.EnableHand());
                        base.CheckError(sm.Init());

                        PXCMHandModule handModule = this.GetHandModule(sm);

                        using (PXCMHandData handData = handModule.CreateOutput())
                        {
                            while (_isRunning && sm.AcquireFrame(true).IsSuccessful())
                            {
                                base.CheckError(handData.Update());

                                ICollection<HandData> data = _handDataService.OnProcess(handData);
                                
                                ICollection<PXCMHandData.AlertType> alert = _realsenseAlertService.OnProcess(handData);

                                _dataService.OnProcess(data, alert);

                                sm.ReleaseFrame();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler exceptionHandler = new ExceptionHandler(ex);
            }
        }

        /// <summary>
        /// Get HandModule Configured
        /// </summary>
        /// <param name="sm">PXCMSenseManager</param>
        /// <returns>PXCMHandModule</returns>
        private PXCMHandModule GetHandModule(PXCMSenseManager sm)
        {
            PXCMHandModule handModule = sm.QueryHand();

            PXCMHandConfiguration handConfig = handModule.CreateActiveConfiguration();
            handConfig.EnableTrackedJoints(true);
            handConfig.SetTrackingMode(PXCMHandData.TrackingModeType.TRACKING_MODE_FULL_HAND);
            handConfig.SetSmoothingValue(1f);
            handConfig.EnableStabilizer(true);
            handConfig.EnableSegmentationImage(true);
            handConfig.SetTrackingBounds(10f, 40f, 8.5f, 8.5f);

            handConfig.EnableAlert(PXCMHandData.AlertType.ALERT_HAND_CALIBRATED);
            handConfig.EnableAlert(PXCMHandData.AlertType.ALERT_HAND_NOT_CALIBRATED);
            handConfig.EnableAlert(PXCMHandData.AlertType.ALERT_HAND_OUT_OF_BORDERS);
            handConfig.EnableAlert(PXCMHandData.AlertType.ALERT_HAND_TOO_CLOSE);
            handConfig.EnableAlert(PXCMHandData.AlertType.ALERT_HAND_TOO_FAR);
            handConfig.EnableAlert(PXCMHandData.AlertType.ALERT_HAND_LOW_CONFIDENCE);

            handConfig.EnableJointSpeed(PXCMHandData.JointType.JOINT_THUMB_TIP, PXCMHandData.JointSpeedType.JOINT_SPEED_AVERAGE, 100);
            handConfig.EnableJointSpeed(PXCMHandData.JointType.JOINT_INDEX_TIP, PXCMHandData.JointSpeedType.JOINT_SPEED_AVERAGE, 100);
            handConfig.EnableJointSpeed(PXCMHandData.JointType.JOINT_MIDDLE_TIP, PXCMHandData.JointSpeedType.JOINT_SPEED_AVERAGE, 100);
            handConfig.EnableJointSpeed(PXCMHandData.JointType.JOINT_PINKY_TIP, PXCMHandData.JointSpeedType.JOINT_SPEED_AVERAGE, 100);
            handConfig.EnableJointSpeed(PXCMHandData.JointType.JOINT_RING_TIP, PXCMHandData.JointSpeedType.JOINT_SPEED_AVERAGE, 100);

            handConfig.ApplyChanges();
            handConfig.Update();

            return handModule;
        }

        /// <summary>
        ///     <para><see cref="IObserver.Notify()"/></para>
        /// </summary>
        public void Notify()
        {
            _isRunning = false;
        }
    }
}
