using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Models
{
    public class BaseModel
    {
        [JsonProperty("i")]
        public ObjectId Id { get; set; }
    }
}
