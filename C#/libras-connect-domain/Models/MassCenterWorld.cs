namespace libras_connect_domain.Models
{
    public class MassCenterWorld : Point3DF32
    {
        public MassCenterWorld(PXCMPoint3DF32 pXCMPoint3DF32)
        {
            X = pXCMPoint3DF32.x;
            Y = pXCMPoint3DF32.y;
            Z = pXCMPoint3DF32.z;
        }

        public MassCenterWorld()
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}
