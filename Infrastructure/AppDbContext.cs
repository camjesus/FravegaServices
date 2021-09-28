using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using FravegaService.Models;
using MongoDB.Driver;

namespace FravegaService.Infrastructure
{
        public class AppDbContext : DbContext
        {
            private readonly IMongoDatabase _db;

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public AppDbContext(IMongoClient client, string dbName)
            {
                _db = client.GetDatabase(dbName);
            }

            public IMongoCollection<PromocionEntity> Promociones => _db.GetCollection<PromocionEntity>("promociones");
        }

}
