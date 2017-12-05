namespace libras_connect_domain.Models
{
    public class JointLocalRotation : Point4DF32
    {
        public JointLocalRotation(PXCMPoint4DF32 localRotation)
        {
            X = localRotation.x;
            Y = localRotation.y;
            W = localRotation.w;
            Z = localRotation.z;
        }

        public JointLocalRotation()
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}
