using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_infrastructure.Image
{
    public static class BitmapUtil
    {
        /// <summary>
        /// Convert Bitmap to Byte Array
        /// </summary>
        /// <param name="image">Bitmap</param>
        /// <returns>Byte array</returns>
        public static byte[] ImageToByte(Bitmap image)
        {
            BitmapData bData = image.LockBits(new Rectangle(new Point(), image.Size), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            int byteCount = bData.Stride * image.Height;
            byte[] bmpBytes = new byte[byteCount];

            Marshal.Copy(bData.Scan0, bmpBytes, 0, byteCount);

            image.UnlockBits(bData);

            return bmpBytes;
        }

        /// <summary>
        /// Convert Byte Array to Bitmap
        /// </summary>
        /// <param name="byteArray">Byte Array</param>
        /// <returns>Bitmap</returns>
        public static Bitmap ByteToImage(byte[] byteArray)
        {
            Bitmap image = null;

            unsafe
            {
                fixed (byte* ptr = byteArray)
                {
                    image = new Bitmap(160, 120, 480, PixelFormat.Format24bppRgb, new IntPtr(ptr));
                }
            }

            return image;
        }
    }
}
