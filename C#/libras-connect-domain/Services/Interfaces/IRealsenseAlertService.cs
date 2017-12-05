using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Services.Interfaces
{
    public interface IRealsenseAlertService
    {
        /// <summary>
        /// Get Alerts
        /// </summary>
        /// <param name="handData">PXCMHandData</param>
        /// <returns>PXCMHandData.AlertType Collection</returns>
        ICollection<PXCMHandData.AlertType> OnProcess(PXCMHandData handData);
    }
}
