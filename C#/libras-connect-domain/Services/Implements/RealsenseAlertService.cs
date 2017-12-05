using libras_connect_domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Services.Implements
{
    /// <summary>
    /// Implementation of IRealsenseAlertService<see cref="IRealsenseAlertService"/>
    /// </summary>
    public class RealsenseAlertService : BaseRealsesenseService, IRealsenseAlertService
    {
        /// <summary>
        ///     <para><see cref="IRealsenseAlertService.OnProcess(PXCMHandData)"/></para>
        /// </summary>
        public ICollection<PXCMHandData.AlertType> OnProcess(PXCMHandData handData)
        {
            ICollection<PXCMHandData.AlertType> alert = new List<PXCMHandData.AlertType>();

            for (int i = 0; i < handData.QueryFiredAlertsNumber(); i++)
            {
                PXCMHandData.AlertData alertData;

                base.CheckError(handData.QueryFiredAlertData(i, out alertData));

                if (!alert.Contains(alertData.label))
                {
                    alert.Add(alertData.label);
                }
            }

            return alert;
        }
    }
}
