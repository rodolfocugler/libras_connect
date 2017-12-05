using libras_connect_domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Repository.Interfaces
{
    public interface IWordDNNRepository
    {
        /// <summary>
        /// Create WordDNN model
        /// </summary>
        /// <param name="wordDNN">WordDNN model</param>
        void Create(WordDNN wordDNN);

        /// <summary>
        /// Get WordDNN model list
        /// </summary>
        /// <returns>WordDNN model collection</returns>
        ICollection<WordDNN> Get();

        /// <summary>
        /// Get WordDNN model by Id
        /// </summary>
        /// <param name="id">ObjectId by WordDNN model</param>
        /// <returns>WordDNN model</returns>
        WordDNN Get(ObjectId id);

        /// <summary>
        /// Delete WordDNN model by Id
        /// </summary>
        /// <param name="id">ObjectId by WordDNN model</param>
        void Delete(ObjectId id);
    }
}
