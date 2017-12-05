using libras_connect_domain.Models;
using System.Collections.Generic;
using System.Drawing;

namespace libras_connect_domain.Services.Interfaces
{
    public interface IDataService
    {
        /// <summary>
        /// Process HandData Collection
        /// </summary>
        /// <param name="handsData">HandData Collection</param>
        /// <param name="alert">PXCMHandData.AlertType Collection</param>
        void OnProcess(ICollection<HandData> handsData, ICollection<PXCMHandData.AlertType> alert);

        /// <summary>
        /// Process Image 
        /// </summary>
        /// <param name="bitmap">Bitmap</param>
        /// <param name="rawBitmap">Raw Bitmap</param>
        void OnProcess(Bitmap bitmap, Bitmap rawBitmap);
    }
}
