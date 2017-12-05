using libras_connect_domain.Models;
using libras_connect_domain.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Repository.Implements.Mongo
{
    /// <summary>
    /// Implementation of ISignalRepository<see cref="IWordDNNRepository"/>
    /// </summary>
    public class WordDNNRepository : CrudRepository<WordDNN>, IWordDNNRepository
    {
        public WordDNNRepository() : base("WordDNN")
        {
        }

        public WordDNNRepository(MongoConnection mongoConnection) : base ("WordDNN", mongoConnection)
        {
        }
    }
}
