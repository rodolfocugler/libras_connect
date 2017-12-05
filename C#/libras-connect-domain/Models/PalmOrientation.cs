namespace libras_connect_domain.Models
{
    public class PalmOrientation : Point4DF32
    {
        public PalmOrientation(PXCMPoint4DF32 pXCMPoint4DF32)
        {
            X = pXCMPoint4DF32.x;
            Y = pXCMPoint4DF32.y;
            Z = pXCMPoint4DF32.z;
            W = pXCMPoint4DF32.w;
        }

        public PalmOrientation()
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}
