using Domain.Core.Data;
using Domain.Core.Exceptions;
using FravegaService.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastucture.Data.Mongo.Repositories
{
    public abstract class MongoRepository<T> : IRepository<T>
        where T : Promotion
    {
        private readonly DataContext _db;
        protected MongoRepository(DataContext db)
        {
            this._db = db;
        }

        protected abstract string CollectionName { get; }
        protected virtual IMongoCollection<T> Collection => _db.Collection<T>(CollectionName);
        protected virtual IMongoQueryable<T> CollectionQuery => _db.Collection<T>(CollectionName).AsQueryable();

        public virtual async Task AddAsync(T entity)
        {
            try
            {
                await Collection.InsertOneAsync(entity);
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                throw new DuplicateEntityException<T>();
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await Collection
                .UpdateOneAsync(n => n.Id.Equals(entity.Id),
                    new BsonDocumentUpdateDefinition<T>(new BsonDocument("$set", BsonDocument.Parse(entity.ToJson()))),
                    new UpdateOptions { IsUpsert = false });
        }
        public virtual async Task<T> FindOneAsync(Guid id)
        {
            return await CollectionQuery.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new EntityNotFoundException<T>(id);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await CollectionQuery.ToListAsync();
        }
    }
}
