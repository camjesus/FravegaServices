using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver.Linq;
using FravegaService.Models;
using Domain.Core.Data;
using Domain.Core.Exceptions;
using System.Threading.Tasks;

namespace Infrastucture.Data.Mongo.Repositories
{
    public class PromotionGenericRepository : PromotionRepository<Promotion>, IPromotionRepository
    {
        public PromotionGenericRepository(DataContext db) : base(db)
        {
        }
        public async Task<Promotion> FindByActivoAndCategoriasProductosInAndMediosDePagoInAndBancosInAsync(IEnumerable<string> CategoriasProductos, IEnumerable<string> MediosDePago, IEnumerable<string> Bancos)
        {
            return await CollectionQuery.FirstOrDefaultAsync(x => x.Activo == true && x.Bancos == Bancos 
                                        && x.CategoriasProductos == CategoriasProductos && x.MediosDePago == MediosDePago);
        }

        public async Task<IEnumerable<Promotion>> FindByActivoAndFechaInicioGreaterThanEqualAndFechaFinLeesThanEqual(DateTime fechaInicio, DateTime fechaFin)
        {
            return (IEnumerable<Promotion>)await CollectionQuery.FirstOrDefaultAsync(x => x.Activo == true && x.FechaInicio >= fechaInicio && x.FechaFin <= fechaFin);
        }

        protected override Task<Promotion> FindDuplicatedEntityAsync(Promotion newEntity)
        {
            throw new NotImplementedException();
        }
    }
}
