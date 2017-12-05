namespace libras_connect_domain.Models
{
    public class JointSpeed : Point3DF32
    {
        public JointSpeed(PXCMPoint3DF32 speed)
        {
            X = speed.x;
            Y = speed.y;
            Z = speed.z;
        }

        public JointSpeed()
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}
