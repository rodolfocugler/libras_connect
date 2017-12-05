namespace libras_connect_domain.Models
{
    public class MassCenterImage : PointF32
    {
        public MassCenterImage(PXCMPointF32 pXCMPointF32)
        {
            X = pXCMPointF32.x;
            Y = pXCMPointF32.y;
        }

        public MassCenterImage()
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}
