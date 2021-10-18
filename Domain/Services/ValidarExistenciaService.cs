using Domain.Core.Data;
using Domain.Core.Exceptions;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;

namespace Domain.Core.Services
{
    public interface IValidarExistenciaService
    {
        void ValidarExistencia(Promotion promo);
    }

    public class ValidarExistenciaService : IValidarExistenciaService
    {
        private readonly IPromotionRepository _promotion;

        public ValidarExistenciaService(IPromotionRepository promotion)
        {
            _promotion = promotion ?? throw new ArgumentNullException(nameof(promotion));
        }

        public void ValidarExistencia(Promotion promotion)
        {
            //_logger.LogInformation("Valido existencia");

            var promociones = _promotion.FindByActivoAndCategoriasProductosInAndMediosDePagoInAndBancosInAsync(promotion.CategoriasProductos, promotion.MediosDePago, promotion.Bancos);
            
            if (promociones != null)
            {
                //_logger.LogError("Error en validacion de Promocion : Ya existe una Promocion para estos bancos, categoria o  medio de pago");
                throw new YaExistePromocionException();
            }
        }
    }
}