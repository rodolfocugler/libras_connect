using System;

namespace libras_connect_domain.Models
{
    public class JointPositionImage : Point3DF32
    {
        public JointPositionImage(PXCMPoint3DF32 positionImage)
        {
            X = positionImage.x;
            Y = positionImage.y;
            Z = positionImage.z;
        }

        public JointPositionImage()
        {
        }

        public override float[] ToArray()
        {
            return new float[]
            {
                this.Round(this.X / 640),
                this.Round(this.Y / 480),
                this.Round(this.Z)
            };
        }


        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}
