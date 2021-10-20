using Entities = FravegaService.Models;
using Domain.Core.Data;
using Domain.Core.Services;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Domain.Core.Services
{
    public interface IUpdatePromocionService
    {
        Task<Guid> UpdatePromocion(PromotionUpd promotion);
    }

    public class UpdatePromocionService : IUpdatePromocionService
    {
        private readonly IValidarPromocionService _validarPromocion;
        private readonly IPromotionRepository _promotion;

        public UpdatePromocionService(
            IValidarPromocionService validarPromocion, 
            IPromotionRepository promotion)
        {
            _validarPromocion = validarPromocion ?? throw new ArgumentNullException(nameof(validarPromocion));
            _promotion = promotion ?? throw new ArgumentNullException(nameof(promotion));
        }

        public async Task<Guid> UpdatePromocion(PromotionUpd promotion)
        {
            //_logger.LogInformation("Update Promocion Id:" + promotion.Id);
            await _validarPromocion.ValidarAsync(promotion);

           var promotionEntity = await _promotion.FindOneAsync(promotion.Id);

            promotionEntity.UpdatePromotion(promotion.MediosDePago, promotion.Bancos, promotion.CategoriasProductos,
                promotion.MaximaCantidadDeCuotas, promotion.ValorInteresesCuotas, promotion.PorcentajeDedescuento, promotion.FechaInicio,
                promotion.FechaFin);

            await _promotion.UpdateAsync(promotionEntity);
            return promotionEntity.Id;
        }
    }
}