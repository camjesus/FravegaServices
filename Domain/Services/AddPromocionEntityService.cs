using Domain.Core.Data;
using FravegaService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FravegaService.Services
{
    public interface IAddPromocionEntityService
    {
        Task<Guid> AddPromocionEntity(Promotion promo);
    }

    public class AddPromocionEntityService : IAddPromocionEntityService
    {
        private readonly ILogger<Promotion> _logger;
        private readonly IPromotionRepository _promotion;
        private readonly ValidarPromocionService _validarPromocion;

        public AddPromocionEntityService(ILogger<Promotion> logger, ValidarPromocionService validarPromocion, IPromotionRepository promotion)
        {
            _logger = logger;
            _validarPromocion = validarPromocion;
            _promotion = promotion;
        }

        public async Task<Guid> AddPromocionEntity(Promotion promo)
        {
            _validarPromocion.ValidarPromocion(promo);

            Promotion promocionEntity = new Promotion(new Guid(), promo.MediosDePago, promo.Bancos, promo.CategoriasProductos, promo.MaximaCantidadDeCuotas,
                promo.ValorInteresesCuotas, promo.PorcentajeDedescuento, promo.FechaInicio, promo.FechaFin, true, DateTime.Now.Date, null);

            await _promotion.AddAsync(promocionEntity);
            return promocionEntity.Id;
        }
    }
}