using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Data;
using FravegaService.Models;
using MongoDB.Driver.Linq;

namespace Infrastucture.Data.Mongo.Repositories
{
    public abstract class PromotionRepository<T> : MongoRepository<T>
      where T : Promotion
    {
        protected override string CollectionName => DataContext.ClientsCollectionName;

        protected override IMongoQueryable<T> CollectionQuery => base.CollectionQuery.OfType<T>();

        protected PromotionRepository(DataContext db)
            : base(db)
        { }
    }
}
