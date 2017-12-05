using libras_connect_domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Repository.Implements.Mongo
{
    public class CrudRepository<T> : BaseRepository where T : BaseModel
    {
        protected string _collectionName;
     
        public CrudRepository(string collectionName)
        {
            _collectionName = collectionName;
        }

        public CrudRepository(string collectionName, MongoConnection mongoConnection) : base(mongoConnection)
        {
            _collectionName = collectionName;
        }

        /// <summary>
        /// Save model in db
        /// </summary>
        /// <param name="t">Model</param>
        public void Create(T t)
        {
            IMongoDatabase database = base.GetDatabase();
            IMongoCollection<T> mongoCollection = database.GetCollection<T>(_collectionName);
            mongoCollection.InsertOne(t);
        }

        /// <summary>
        /// Get Collection
        /// </summary>
        /// <returns>Model Collection</returns>
        public ICollection<T> Get()
        {
            IMongoDatabase database = base.GetDatabase();
            IMongoCollection<T> mongoCollection = database.GetCollection<T>(_collectionName);
            ICollection<T> list = mongoCollection.Find<T>(FilterDefinition<T>.Empty).ToList();

            return list;
        }

        /// <summary>
        /// Get Model by ObjectId
        /// </summary>
        /// <param name="id">ObjectId</param>
        /// <returns>Model</returns>
        public T Get(ObjectId id)
        {
            IMongoDatabase database = base.GetDatabase();
            IMongoCollection<T> mongoCollection = database.GetCollection<T>(_collectionName);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(f => f.Id, id);
            T t = mongoCollection.Find<T>(filter).SingleOrDefault();

            return t;
        }

        /// <summary>
        /// Delete Model by ObjectId
        /// </summary>
        /// <param name="id">ObjectId</param>
        public void Delete(ObjectId id)
        {
            IMongoDatabase database = base.GetDatabase();
            IMongoCollection<T> mongoCollection = database.GetCollection<T>(_collectionName);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(f => f.Id, id);
            mongoCollection.FindOneAndDelete<T>(filter);
        }
    }
}
