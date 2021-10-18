using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FravegaService.Models;

namespace Domain.Core.Data
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
        Task<Promotion> FindByActivoAndCategoriasProductosInAndMediosDePagoInAndBancosInAsync(IEnumerable<string> CategoriasProductos, IEnumerable<string> MediosDePago, IEnumerable<string> Bancos);
        Task<IEnumerable<Promotion>> FindByActivoAndFechaInicioGreaterThanEqualAndFechaFinLeesThanEqual(DateTime fechaInicio, DateTime fechaFin);
    }

    public class PromotionRepository : IPromotionRepository
    {
        public PromotionRepository()
        {
        }

        public async Task<Promotion> FindByActivoAndCategoriasProductosInAndMediosDePagoInAndBancosInAsync(IEnumerable<string> CategoriasProductos, IEnumerable<string> MediosDePago, IEnumerable<string> Bancos)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Promotion>> FindByActivoAndFechaInicioGreaterThanEqualAndFechaFinLeesThanEqual(DateTime fechaInicio, DateTime fechaFin)
        {
            throw new NotImplementedException();
        }

        public Task<List<Promotion>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Promotion> FindOneAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Promotion promotion)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Promotion promotion)
        {
            throw new NotImplementedException();
        }

        public Task<Promotion> FindOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
