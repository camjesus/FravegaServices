using Domain.Core.Data;
using Entities = FravegaService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using FravegaService.Domain.Core.DTO;
using Domain.Core.Services;

namespace FravegaService.Services
{
    public interface IAddPromocionEntityService
    {
        Task<Guid> AddPromocionEntity(Promotion promo);
    }

    public class AddPromocionEntityService : IAddPromocionEntityService
    {
        private readonly IValidarCrearPromocionService _validarCrearPromocionService;
        private readonly IPromotionRepository _promotion;


        public AddPromocionEntityService(
            IValidarCrearPromocionService validarCrearPromocionService,
            IPromotionRepository promotion)
        {
            _validarCrearPromocionService = validarCrearPromocionService ?? throw new ArgumentNullException(nameof(validarCrearPromocionService));
            _promotion = promotion ?? throw new ArgumentNullException(nameof(promotion));
        }

        public async Task<Guid> AddPromocionEntity(Promotion promo)
        {
            await _validarCrearPromocionService.ValidarAsync(promo);

            var promocionEntity = new Entities.Promotion(promo.MediosDePago, promo.Bancos, promo.CategoriasProductos, promo.MaximaCantidadDeCuotas,
                promo.ValorInteresesCuotas, promo.PorcentajeDedescuento, promo.FechaInicio, promo.FechaFin);

            await _promotion.AddAsync(promocionEntity);
            
            return promocionEntity.Id;
        }
    }
}