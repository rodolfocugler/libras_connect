namespace libras_connect_domain.Models
{
    public class JointPositionWorld : Point3DF32
    {
        public JointPositionWorld(PXCMPoint3DF32 positionWorld)
        {
            X = positionWorld.x;
            Y = positionWorld.y;
            Z = positionWorld.z;
        }

        public JointPositionWorld()
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}
