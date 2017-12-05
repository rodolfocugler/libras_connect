namespace libras_connect_domain.Models
{
    public class BoundingBox : RectF32
    {
        public BoundingBox(PXCMRectI32 pXCMRectI32)
        {
            X = pXCMRectI32.x;
            Y = pXCMRectI32.y;
            W = pXCMRectI32.w;
            H = pXCMRectI32.h;
        }

        public BoundingBox()
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}
