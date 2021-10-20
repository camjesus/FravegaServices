using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Data;
using FravegaService.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastucture.Data.Mongo.Repositories
{
    public class PromotionRepository : MongoRepository<Promotion>, IPromotionRepository
    {
        protected override string CollectionName => DataContext.ClientsCollectionName;
        public PromotionRepository(DataContext db)
            : base(db)
        { }

        public async Task<Promotion> FindByActivoAndCategoriasProductosInAndMediosDePagoInAndBancosInAsync(IEnumerable<string> CategoriasProductos, IEnumerable<string> MediosDePago, IEnumerable<string> Bancos)
        {
            return await CollectionQuery
                .Where(x => x.Activo)
                .Where(x => x.Bancos == Bancos)
                .Where(x => x.CategoriasProductos == CategoriasProductos)
                .Where(x => x.MediosDePago == MediosDePago)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Promotion>> FindPromocionesVigentesFilterAsync(IEnumerable<string> categoriasProductos, IEnumerable<string> mediosDePago, IEnumerable<string> bancos)
        {
            return await CollectionQuery
                .Where(x => x.Activo)
                .Where(x => x.FechaInicio >= DateTime.Now)
                .Where(x => x.FechaFin <= DateTime.Now)
                .Where(x => x.CategoriasProductos == categoriasProductos)
                .Where(x => x.Bancos == bancos)
                .Where(x => x.MediosDePago == mediosDePago)
                .ToListAsync();
        }

        public async Task<IEnumerable<Promotion>> FindPromocionesVigentesAsync(DateTime date)
        {
            date = date == null ? DateTime.Now : date;

            return await CollectionQuery
                .Where(x => x.Activo)
                .Where(x => x.FechaInicio >= date).ToListAsync();
        }

        public async Task<Promotion> FindOneAsync(string hash)
        {
            return await CollectionQuery.FirstOrDefaultAsync(x => x.Hash == hash);
        }
    }


}
