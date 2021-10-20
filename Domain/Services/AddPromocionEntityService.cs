using Domain.Core.Data;
using Entities = FravegaService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using FravegaService.Domain.Core.DTO;
using Domain.Core.Services;
using Domain.Core.Exceptions;

namespace Domain.Core.Services
{
    public interface IAddPromocionEntityService
    {
        Task<Guid> AddPromocionEntity(Promotion promo);
    }

    public class AddPromocionEntityService : IAddPromocionEntityService
    {
        private readonly IValidarPromocionService _validarCrearPromocionService;
        private readonly IPromotionRepository _promotion;


        public AddPromocionEntityService(
            IValidarPromocionService validarPromocionService,
            IPromotionRepository promotion)
        {
            _validarCrearPromocionService = validarPromocionService ?? throw new ArgumentNullException(nameof(validarPromocionService));
            _promotion = promotion ?? throw new ArgumentNullException(nameof(promotion));
        } 

        public async Task<Guid> AddPromocionEntity(Promotion promo) //insert positivo
        {
            await _validarCrearPromocionService.ValidarAsync(promo);

            var promocionEntity = new Entities.Promotion(promo.MediosDePago, promo.Bancos, promo.CategoriasProductos, promo.MaximaCantidadDeCuotas,
                promo.ValorInteresesCuotas, promo.PorcentajeDedescuento, promo.FechaInicio, promo.FechaFin);

            try
            {
                await _promotion.AddAsync(promocionEntity);
                return promocionEntity.Id;
            }
            catch (DuplicateEntityException)
            {
                var duplicatedPromotion = await _promotion.FindOneAsync(promocionEntity.Hash);
                return duplicatedPromotion.Id;
            }
        }

        public async Task<Guid> AddPromocionEntity2(Promotion promo) //negativo
        {
            await _validarCrearPromocionService.ValidarAsync(promo);

            var promocionEntity = new Entities.Promotion(promo.MediosDePago, promo.Bancos, promo.CategoriasProductos, promo.MaximaCantidadDeCuotas,
                promo.ValorInteresesCuotas, promo.PorcentajeDedescuento, promo.FechaInicio, promo.FechaFin);

            var duplicatedPromotion = await _promotion.FindOneAsync(promocionEntity.Hash);

            if(duplicatedPromotion == null)
            {
                await _promotion.AddAsync(promocionEntity);
                return promocionEntity.Id;
            }
            else
            {
                return duplicatedPromotion.Id;
            }
        }
    }
}