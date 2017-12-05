using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Repository.Implements.Mongo
{
    public class MongoConnection
    {
        public MongoConnection(string host, int port, string databaseName)
        {
            this.Host = host;
            this.Port = port;
            this.DatabaseName = databaseName;
        }

        public MongoConnection(string connection)
        {
            string[] parameters = connection.Split(':');
            this.Host = parameters[0];
            this.Port = Convert.ToInt32(parameters[1]);

            parameters = parameters[1].Split('/');
            this.DatabaseName = parameters[1];
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public string DatabaseName { get; set; }
    }
}
