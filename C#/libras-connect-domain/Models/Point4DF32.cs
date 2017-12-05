using Newtonsoft.Json;
using System;

namespace libras_connect_domain.Models
{
    public class Point4DF32 
    {
        [JsonProperty("x")]
        public Single X { get; set; }
        [JsonProperty("y")]
        public Single Y { get; set; }
        [JsonProperty("z")]
        public Single Z { get; set; }
        [JsonProperty("w")]
        public Single W { get; set; }

        public float[] ToArray()
        {
            return new float[] 
            {
                this.Round(this.X),
                this.Round(this.Y),
                this.Round(this.Z),
                this.Round(this.W)
            };
        }

        public float Round(float value)
        {
            return (float)Math.Round(value, 8);
        }

        public override string ToString()
        {
            return string.Format("X=[{0}]; Y=[{1}]; Z=[{2}]; W=[{3}];", X, Y, Z, W);
        }
    }
}
