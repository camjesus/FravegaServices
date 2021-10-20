using FravegaService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core.Data
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
        Task<Promotion> FindByActivoAndCategoriasProductosInAndMediosDePagoInAndBancosInAsync(IEnumerable<string> CategoriasProductos, IEnumerable<string> MediosDePago, IEnumerable<string> Bancos);

        Task<IEnumerable<Promotion>> FindPromocionesVigentesFilterAsync(IEnumerable<string> categoriasProductos, IEnumerable<string> mediosDePago, IEnumerable<string> bancos);

        Task<IEnumerable<Promotion>> FindPromocionesVigentesAsync(DateTime date);

        Task<Promotion> FindOneAsync(string hash);
    }
}