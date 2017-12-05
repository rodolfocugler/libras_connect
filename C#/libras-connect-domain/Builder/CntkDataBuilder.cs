using AForge.Imaging.Filters;
using libras_connect_domain.Enums;
using libras_connect_domain.Models;
using libras_connect_infrastructure.Image;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Builder
{
    public static class CntkDataBuilder
    {
        public static List<float> Build(ICollection<HandData> handData)
        {
            List<float> list = new List<float>();

            list.AddRange(CntkDataBuilder.Build(handData, HandEnum.RIGHT));
            list.AddRange(CntkDataBuilder.Build(handData, HandEnum.LEFT));

            return list;
        }

        public static List<float> Build(Bitmap bitmap)
        {
            List<float> list = new List<float>();

            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 24; y++)
                {
                    list.Add((float)bitmap.GetPixel(x, y).R / (float)255);
                }
            }
            
            return list;
        }

        /// <summary>
        /// Get hand data as a vector
        /// </summary>
        /// <param name="handData">HandData model</param>
        /// <param name="handEnum">HandEnum</param>
        /// <returns>float collection</returns>
        private static ICollection<float> Build(ICollection<HandData> handData, HandEnum handEnum)
        {
            if (handData == null)
            {
                return new float[340];
            }

            List<float> list = new List<float>();
            HandData hd = null;

            for (int i = 0; i < handData.Count; i++)
            {
                if (handData.ElementAt(i).HandEnum == handEnum)
                {
                    hd = handData.ElementAt(i);
                    break;
                }
            }

            if (hd == null)
            {
                return new float[340];
            }
            
            for (int i = 3; i < 23; i++)
            {
                JointData jd = null;

                if (hd.JointDatas.TryGetValue((JointEnum)i, out jd))
                {
                    list.AddRange(jd.JointPositionWorld.ToArray());
                    list.AddRange(jd.JointPositionImage.ToArray());
                    list.AddRange(jd.JointLocalRotation.ToArray());
                    list.AddRange(jd.JointGlobalOrientation.ToArray());
                    list.AddRange(jd.JointSpeed.ToArray());
                }
                else
                {
                    list.AddRange(new float[17]);
                }
            }

            return list;
        }
    }
}
