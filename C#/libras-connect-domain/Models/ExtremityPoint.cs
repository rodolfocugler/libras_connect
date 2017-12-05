using Newtonsoft.Json;
using libras_connect_domain.Enums;

namespace libras_connect_domain.Models
{
    public class ExtremityPoint
    {
        [JsonProperty("ete")]
        public ExtremityTypeEnum ExtremityTypeEnum { get; set; }
        [JsonProperty("epw")]
        public ExtremityPointWorld ExtremityPointWorld { get; set; }
        [JsonProperty("epi")]
        public ExtremityPointImage ExtremityPointImage { get; set; }
        [JsonProperty("hd")]
        public HandData HandData { get; set; }

        public override string ToString()
        {
            return string.Format("ExtremityTypeEnum=[{0}]; ExtremityPointWorld=[{1}]; ExtremityPointImage=[{2}]; HandData=[{3}];",
                ExtremityTypeEnum, ExtremityPointWorld, ExtremityPointImage, HandData);
        }
    }
}
