using FravegaService.Models;
using Infrastucture.Data.Mongo.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Infrastucture.Data.Mongo
{
    public class DataContext
    {
        private readonly IMongoDatabase _db;

        internal static string ClientsCollectionName => "promotions";

        public DataContext(MongoClient client, IOptions<MongoOptions> options)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (options == null || options.Value == null) throw new ArgumentNullException(nameof(options));

            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<T> Collection<T>(string name) => _db.GetCollection<T>(name);

        public async Task CreateIndexesAsync()
        {
            var hashUniqueIndex = new CreateIndexModel<Promotion>(
                Builders<Promotion>.IndexKeys.Ascending(x => x.Hash),
                new CreateIndexOptions<Promotion>()
                {
                    Unique = true
                });

            await _db.GetCollection<Promotion>(ClientsCollectionName).Indexes.CreateOneAsync(hashUniqueIndex);
        }
    }
}