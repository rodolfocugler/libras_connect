using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Repository.Implements.Mongo
{
    /// <summary>
    /// Base Repository
    /// </summary>
    public abstract class BaseRepository
    {
        private MongoConnection _mongoConnection;
     
        public BaseRepository()
        {
            _mongoConnection = new MongoConnection("127.0.0.1", 27017, "libras_connect");
        }

        public BaseRepository(MongoConnection mongoConnection)
        {
            _mongoConnection = mongoConnection;
        }

        /// <summary>
        /// Get MongoClientSettings 
        /// </summary>
        /// <returns>MongoClientSettings</returns>
        public MongoClientSettings GetSettings()
        {
            MongoClientSettings mongoClientSettings = new MongoClientSettings();
            mongoClientSettings.WaitQueueSize = int.MaxValue;
            mongoClientSettings.WaitQueueTimeout = new TimeSpan(0, 2, 0);
            mongoClientSettings.MinConnectionPoolSize = 1;
            mongoClientSettings.MaxConnectionPoolSize = 25;

            return mongoClientSettings;
        }

        /// <summary>
        /// Get IMongoDatabase
        /// </summary>
        /// <returns>IMongoDatabase</returns>
        public IMongoDatabase GetDatabase()
        {
            MongoClientSettings mongoClientSettings = this.GetSettings();
            mongoClientSettings.Server = new MongoServerAddress(_mongoConnection.Host, _mongoConnection.Port);
            IMongoClient client = new MongoClient(mongoClientSettings);
            IMongoDatabase mongoDatabase = client.GetDatabase(_mongoConnection.DatabaseName);

            return mongoDatabase;
        }
    }
}
