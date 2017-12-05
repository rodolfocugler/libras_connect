namespace libras_connect_domain.Models
{
    public class JointGlobalOrientation : Point4DF32
    {
        public JointGlobalOrientation(PXCMPoint4DF32 globalOrientation)
        {
            X = globalOrientation.x;
            Y = globalOrientation.y;
            W = globalOrientation.w;
            Z = globalOrientation.z;
        }

        public JointGlobalOrientation()
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}
