using Newtonsoft.Json;
using System;

namespace libras_connect_domain.Models
{
    public class PointF32
    {
        [JsonProperty("x")]
        public Single X { get; set; }
        [JsonProperty("y")]
        public Single Y { get; set; }

        public override string ToString()
        {
            return string.Format("X=[{0}]; Y=[{1}];", X, Y);
        }
    }
}
