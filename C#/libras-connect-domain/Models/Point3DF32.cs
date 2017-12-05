using Newtonsoft.Json;
using System;

namespace libras_connect_domain.Models
{
    public class Point3DF32
    {
        [JsonProperty("x")]
        public Single X { get; set; }
        [JsonProperty("y")]
        public Single Y { get; set; }
        [JsonProperty("z")]
        public Single Z { get; set; }

        public virtual float[] ToArray()
        {
            return new float[]
            {
                this.Round(this.X),
                this.Round(this.Y),
                this.Round(this.Z)
            };
        }

        public float Round(float value)
        {
            return (float)Math.Round(value, 8);
        }

        public override string ToString()
        {
            return string.Format("X=[{0}]; Y=[{1}]; Z=[{2}];", X, Y, Z);
        }
    }
}
