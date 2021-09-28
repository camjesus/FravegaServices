using AutoMapper;
using Domain.Core.Data;
using FravegaService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FravegaService.Services
{
    public interface IUpdatePromocionService
    {
        Task<Guid> UpdatePromocion(Promotion promo);
    }

    public class UpdatePromocionService : IUpdatePromocionService
    {
        private readonly ILogger<Promotion> _logger;
        private readonly ValidarPromocionService _validarPromocion;
        private readonly IPromotionRepository _promotion;
        private readonly IMapper _mappper;

        public UpdatePromocionService(ILogger<Promotion> logger, ValidarPromocionService validarPromocion, IPromotionRepository promotion)
        {
            _logger = logger;
            _validarPromocion = validarPromocion;
            _promotion = promotion;
        }

        public async Task<Guid> UpdatePromocion(Promotion promo)
        {
            _validarPromocion.ValidarPromocion(promo);

            Promotion promoUpd = new Promotion(promo.Id, promo.MediosDePago, promo.Bancos, promo.CategoriasProductos, promo.MaximaCantidadDeCuotas,
                promo.ValorInteresesCuotas, promo.PorcentajeDedescuento, promo.FechaInicio, promo.FechaFin, true, promo.FechaCreacion, DateTime.Now);

            await _promotion.Update(promoUpd);
            return promo.Id;
        }
    }
}