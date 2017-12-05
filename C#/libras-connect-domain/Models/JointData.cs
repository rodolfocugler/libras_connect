using Newtonsoft.Json;
using libras_connect_domain.Enums;

namespace libras_connect_domain.Models
{
    public class JointData
    {
        public JointData(PXCMHandData.JointData jointData, int jointEnum)
        {
            JointEnum = (JointEnum)jointEnum;
            Confidence = jointData.confidence;
            JointPositionWorld = new JointPositionWorld(jointData.positionWorld);
            JointPositionImage = new JointPositionImage(jointData.positionImage);
            JointLocalRotation = new JointLocalRotation(jointData.localRotation);
            JointGlobalOrientation = new JointGlobalOrientation(jointData.globalOrientation);
            JointSpeed = new JointSpeed(jointData.speed);
        }

        public JointData()
        {
        }

        [JsonProperty("je")]
        public JointEnum JointEnum { get; set; }
        [JsonProperty("c")]
        public double Confidence { get; set; }
        [JsonProperty("jpw")]
        public JointPositionWorld JointPositionWorld { get; set; }
        [JsonProperty("jpi")]
        public JointPositionImage JointPositionImage { get; set; }
        [JsonProperty("jlr")]
        public JointLocalRotation JointLocalRotation { get; set; }
        [JsonProperty("jgo")]
        public JointGlobalOrientation JointGlobalOrientation { get; set; }
        [JsonProperty("js")]
        public JointSpeed JointSpeed { get; set; }

        public override string ToString()
        {
            return string.Format("JointEnum=[{0}]; Confidence=[{1}]; JointPositionWorld=[{2}]; JointPositionImage=[{3}]; JointLocalRotation=[{4}]; JointGlobalOrientation=[{5}]; JointSpeed=[{6}];",
                JointEnum, Confidence, JointPositionWorld, JointPositionImage, JointLocalRotation, JointGlobalOrientation, JointSpeed);
        }
    }
}
