using libras_connect_domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Models
{
    public class Signal : BaseModel
    {
        public Signal(string word, int frameNumber)
        {
            this.FrameNumber = frameNumber;
            this.Word = word;
            this.DateTimeServer = DateTime.UtcNow;
        }

        public Signal()
        {
            this.DateTimeServer = DateTime.UtcNow;
        }

        [JsonProperty("dtci")]
        public DateTime DateTimeClientImage { get; set; }
        [JsonProperty("dtcd")]
        public DateTime DateTimeClientData { get; set; }
        [JsonProperty("dts")]
        public DateTime DateTimeServer { get; set; }
        [JsonProperty("w")]
        public string Word { get; set; }
        [JsonProperty("h")]
        public ICollection<HandData> HandData { get; set; }
        [JsonProperty("i")]
        public byte[] Image { get; set; }
        [JsonProperty("fn")]
        public int FrameNumber { get; set; }

        [BsonIgnore]
        public Bitmap Bitmap { get; set; }
        [BsonIgnore]
        public IList<float> DataFloat { get; set; }
        [BsonIgnore]
        public IList<float> ImageFloat { get; set; }

        public virtual IList<float> CntkInput
        {
            get
            {
                List<float> list = new List<float>();
                list.AddRange(this.DataFloat);
                list.AddRange(this.ImageFloat);

                return list;
            }
        }

        public void SetData(DataSocket dataSocket)
        {
            this.HandData = dataSocket.HandData;
            this.DateTimeClientData = dataSocket.DateTime;
        }

        public void SetImage(DataSocket dataSocket)
        {
            this.DateTimeClientImage = dataSocket.DateTime;
            this.Image = dataSocket.Image;
        }
    }
}
