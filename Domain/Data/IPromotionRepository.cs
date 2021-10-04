using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FravegaService.Models;

namespace Domain.Core.Data
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
        Task<IEnumerable<Promotion>> FindByActivoAndCategoriasProductosInAndMediosDePagoInAndBancosInAsync(bool activo, IEnumerable<string> CategoriasProductos, IEnumerable<string> MediosDePago, IEnumerable<string> Bancos);
        Task<IEnumerable<Promotion>> FindByActivoAndFechaInicioGreaterThanEqualAndFechaFinLeesThanEqual(bool activo, DateTime fechaInicio, DateTime fechaFin);

    }

}
