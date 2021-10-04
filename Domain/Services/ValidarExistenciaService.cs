using Domain.Core.Data;
using Domain.Core.Exceptions;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;

namespace FravegaService.Services
{
    public interface IValidarExistenciaService
    {
        void ValidarExistencia(Promotion promo);
    }

    public class ValidarExistenciaService : IValidarExistenciaService
    {
        private readonly ILogger<Promotion> _logger;
        private readonly IPromotionRepository _promotion;

        public ValidarExistenciaService(ILogger<Promotion> logger, IPromotionRepository promotion)
        {
            _logger = logger;
            _promotion = promotion;
        }

        public void ValidarExistencia(Promotion promotion)
        {
            _logger.LogInformation("Valido existencia");

            var promociones = _promotion.FindByActivoAndCategoriasProductosInAndMediosDePagoInAndBancosInAsync(true, promotion.CategoriasProductos, promotion.MediosDePago, promotion.Bancos);
            
            if (promociones != null)
            {
                _logger.LogError("Error en validacion de Promocion : Ya existe una Promocion para estos bancos, categoria o  medio de pago");
                throw new YaExistePromocionException();
            }
        }
    }
}