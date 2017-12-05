using libras_connect_client.Services.Interfaces;
using libras_connect_domain.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static PXCMImage;

namespace libras_connect_client.Services.Implements
{
    /// <summary>
    /// Implementation of IBitmapService<see cref="IBitmapService"/>
    /// </summary>
    public class BitmapService : IBitmapService
    {
        /// <summary>
        ///     <para><see cref=IBitmapService.PaintImage(Bitmap)"/></para>
        /// </summary>
        public BitmapImage PaintImage(Bitmap image)
        {
            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream memory = new MemoryStream())
            {
                image.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        /// <summary>
        ///     <para><see cref="IBitmapService.PaintImageCamera(ICollection{realsense_domain.Domain.Models.HandData})"/></para>
        /// </summary>
        public Bitmap PaintImageCamera(ICollection<HandData> handsData)
        {
            Bitmap image = new Bitmap(600, 600);

            using (Graphics g = Graphics.FromImage(image))
            {
                using (Pen boneColor = new Pen(Color.Gold, 3.0f))
                {
                    foreach (HandData handData in handsData)
                    {
                        int baseX = 0, baseY = 0, wristX = 0, wristY = 0, i = 0;

                        foreach (JointData jointData in handData.JointDatas.Values)
                        {
                            if (i == 0)
                            {
                                baseX = (int)jointData.JointPositionImage.X;
                                baseY = (int)jointData.JointPositionImage.Y;

                                wristX = (int)jointData.JointPositionImage.X;
                                wristY = (int)jointData.JointPositionImage.Y;
                                i++;
                            }
                            else
                            {
                                int x = (int)jointData.JointPositionImage.X;
                                int y = (int)jointData.JointPositionImage.Y;

                                if (jointData.Confidence <= 0)
                                {
                                    continue;
                                }

                                int jointEnum = (int)jointData.JointEnum;

                                if (jointEnum == 2 || jointEnum == 6 || jointEnum == 10 || jointEnum == 14 || jointEnum == 18)
                                {
                                    baseX = wristX;
                                    baseY = wristY;
                                }

                                g.DrawLine(boneColor, new Point(baseX, baseY), new Point(x, y));
                                baseX = x;
                                baseY = y;

                                using (Pen red = new Pen(Color.Red, 3.0f),
                                       black = new Pen(Color.Black, 3.0f),
                                       green = new Pen(Color.Green, 3.0f),
                                       blue = new Pen(Color.Blue, 3.0f),
                                       cyan = new Pen(Color.Cyan, 3.0f),
                                       yellow = new Pen(Color.Yellow, 3.0f),
                                       orange = new Pen(Color.Orange, 3.0f))
                                {
                                    Pen currnetPen = black;

                                    float sz = 4;

                                    if (jointEnum == 1)
                                    {
                                        currnetPen = red;
                                        sz += 4;
                                    }
                                    else if (jointEnum == 2 || jointEnum == 3 || jointEnum == 4 || jointEnum == 5)
                                    {
                                        currnetPen = green;
                                    }
                                    else if (jointEnum == 6 || jointEnum == 7 || jointEnum == 8 || jointEnum == 9)
                                    {
                                        currnetPen = blue;
                                    }
                                    else if (jointEnum == 10 || jointEnum == 11 || jointEnum == 12 || jointEnum == 13)
                                    {
                                        currnetPen = yellow;
                                    }
                                    else if (jointEnum == 14 || jointEnum == 15 || jointEnum == 16 || jointEnum == 17)
                                    {
                                        currnetPen = cyan;
                                    }
                                    else if (jointEnum == 18 || jointEnum == 19 || jointEnum == 20 || jointEnum == 21)
                                    {
                                        currnetPen = orange;
                                    }

                                    if (jointEnum == 5 || jointEnum == 9 || jointEnum == 13 || jointEnum == 17 || jointEnum == 21)
                                    {
                                        sz += 4;
                                    }

                                    g.DrawEllipse(currnetPen, x - sz / 2, y - sz / 2, sz, sz);
                                }
                            }
                        }
                    }
                }
            }

            return image;
        }
    }
}
