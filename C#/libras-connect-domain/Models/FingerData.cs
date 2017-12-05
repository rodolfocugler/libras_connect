using Newtonsoft.Json;
using libras_connect_domain.Enums;
using System;

namespace libras_connect_domain.Models
{
    public class FingerData 
    {
        public FingerData(PXCMHandData.FingerData fingerData, int fingerType)
        {
            Radius = fingerData.radius;
            Folderness = fingerData.foldedness;
            FingerTypeEnum = (FingerTypeEnum)fingerType;
        }

        public FingerData()
        {            
        }

        [JsonProperty("fte")]
        public FingerTypeEnum FingerTypeEnum { get; set; }
        [JsonProperty("f")]
        public int Folderness { get; set; }
        [JsonProperty("r")]
        public Single Radius { get; set; }
        
        public override string ToString()
        {
            return string.Format("FingerTypeEnum=[{0}]; Folderness=[{1}]; Radius=[{2}];", FingerTypeEnum, Folderness, Radius);
        }
    }
}
