using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_infrastructure.Serialize
{
    public static class SerializeUtil
    {
        /// <summary>
        /// Serialize an object
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="obj">Object to convert</param>
        /// <returns>Json</returns>
        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserialize json
        /// </summary>
        /// <typeparam name="T">Object to convert</typeparam>
        /// <param name="text">Json</param>
        /// <returns>Object converted</returns>
        public static T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
