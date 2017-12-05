using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using libras_connect_domain.Models;
using libras_connect_domain.Repository.Interfaces;
using MongoDB.Bson;

namespace libras_connect_domain.Repository.Implements.Mongo
{
    /// <summary>
    /// Implementation of ISignalRepository<see cref="ISignalRepository"/>
    /// </summary>
    public class SignalRepository : CrudRepository<Signal>, ISignalRepository
    {
        public SignalRepository() : base("Signal")
        {
        }

        public SignalRepository(MongoConnection mongoConnection) : base ("Signal", mongoConnection)
        {
        }

        /// <summary>
        ///     <para><see cref="ISignalRepository.GetLastSignals()"/></para>
        /// </summary>
        public ICollection<Signal> GetLastSignals()
        {
            IMongoDatabase database = base.GetDatabase();
            IMongoCollection<Signal> mongoCollection = database.GetCollection<Signal>(base._collectionName);

            Signal signal = mongoCollection
                                .AsQueryable<Signal>()
                                .Where(s => s.FrameNumber == 0)
                                .OrderByDescending(s => s.DateTimeServer)
                                .FirstOrDefault();

            var fields = Builders<Signal>.Projection
                                         .Include(s => s.Id)
                                         .Include(s => s.Word);

            return mongoCollection
                            .Find(s => s.DateTimeServer > signal.DateTimeServer)
                            .Project<Signal>(fields)
                            .ToList();
        }
    }
}
