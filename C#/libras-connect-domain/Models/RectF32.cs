using Newtonsoft.Json;
using System;

namespace libras_connect_domain.Models
{
    public class RectF32
    {
        [JsonProperty("x")]
        public Single X { get; set; }
        [JsonProperty("y")]
        public Single Y { get; set; }
        [JsonProperty("w")]
        public Single W { get; set; }
        [JsonProperty("h")]
        public Single H { get; set; }

        public override string ToString()
        {
            return string.Format("X=[{0}]; Y=[{1}]; W=[{2}]; H=[{3}];", X, Y, W, H);
        }
    }
}
