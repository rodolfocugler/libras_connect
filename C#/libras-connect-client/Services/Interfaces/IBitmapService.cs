using libras_connect_domain.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace libras_connect_client.Services.Interfaces
{
    /// <summary>
    /// Interface of BitmapService
    /// </summary>
    public interface IBitmapService
    {
        /// <summary>
        /// Paint Bitmap
        /// </summary>
        /// <param name="handsData">HandData Collection</param>
        /// <param name="image">Bitmap</param>
        /// <returns>Bitmap by data</returns>
        Bitmap PaintImageCamera(ICollection<HandData> handsData);

        /// <summary>
        /// Get BitmapImage to paint screen
        /// </summary>
        /// <param name="image">Bitmap built</param>
        /// <returns>BitmapImage</returns>
        BitmapImage PaintImage(Bitmap image);
    }
}
