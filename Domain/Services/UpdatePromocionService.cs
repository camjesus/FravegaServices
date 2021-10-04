using Entities = FravegaService.Models;
using Domain.Core.Data;
using Domain.Core.Services;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FravegaService.Services
{
    public interface IUpdatePromocionService
    {
        Task<Guid> UpdatePromocion(Promotion promotion);
    }

    public class UpdatePromocionService : IUpdatePromocionService
    {
        private readonly ILogger<Promotion> _logger;
        private readonly ValidarPromocionService _validarPromocion;
        private readonly IPromotionRepository _promotion;

        public UpdatePromocionService(
            ILogger<Promotion> logger, 
            ValidarPromocionService validarPromocion, 
            IPromotionRepository promotion)
        {
            _logger = logger;
            _validarPromocion = validarPromocion;
            _promotion = promotion;
        }

        public async Task<Guid> UpdatePromocion(Promotion promotion)
        {
            _logger.LogInformation("Update Promocion Id:" + promotion.Id);
            await _validarPromocion.ValidarAsync(promotion);

           var promotionEntity = await _promotion.FindOneAsync(promotion.Id);

            promotionEntity.UpdatePromotion(promotion.MediosDePago, promotion.MediosDePago, promotion.CategoriasProductos,
                promotion.MaximaCantidadDeCuotas, promotion.ValorInteresesCuotas, promotion.PorcentajeDedescuento, promotion.FechaInicio,
                promotion.FechaFin);

            await _promotion.UpdateAsync(promotionEntity);
            return promotion.Id;
        }
    }
}