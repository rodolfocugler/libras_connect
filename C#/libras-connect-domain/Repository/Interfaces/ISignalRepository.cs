using libras_connect_domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Repository.Interfaces
{
    public interface ISignalRepository
    {
        /// <summary>
        /// Create Signal model
        /// </summary>
        /// <param name="signal">Signal model</param>
        void Create(Signal signal);

        /// <summary>
        /// Get Signal model list
        /// </summary>
        /// <returns>Signal model collection</returns>
        ICollection<Signal> Get();

        /// <summary>
        /// Get Signal model by Id
        /// </summary>
        /// <param name="id">ObjectId by Signal model</param>
        /// <returns>Signal model</returns>
        Signal Get(ObjectId id);

        /// <summary>
        /// Delete Signal model by Id
        /// </summary>
        /// <param name="id">ObjectId by Signal model</param>
        void Delete(ObjectId id);

        /// <summary>
        /// Get Last Signals Inserted
        /// </summary>
        /// <returns>Signal Collection</returns>
        ICollection<Signal> GetLastSignals();
    }
}
