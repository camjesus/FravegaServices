using Domain.Core.Data;
using FravegaService.Models;
using Microsoft.Extensions.Logging;
using System;

namespace FravegaService.Services
{
    public interface IValidarExistencia
    {
        void ValidarPromocion(Promotion promo);
    }

    public class ValidarPromocionService : IValidarExistencia
    {
        private readonly ILogger<Promotion> _logger;
        private readonly IPromotionRepository _promotion;

        public ValidarPromocionService(ILogger<Promotion> logger, IPromotionRepository promotion)
        {
            _logger = logger;
            _promotion = promotion;
        }

        public void ValidarPromocion(Promotion promo)
        {
            var promociones = _promotion.FindAllAsync(x => x.Activo == true && x.FechaFin <= DateTime.Now && x.FechaInicio >= DateTime.Now
            && x.CategoriasProductos == promo.CategoriasProductos && x.MediosDePago == promo.MediosDePago && x.Bancos == promo.Bancos);

            if (promociones != null)
            {
                _logger.LogError("Error en validacion de Promocion : Ya existe una Promocion para estos bancos, categoria o  medio de pago");
                throw new Exception("Ya existe una Promocion para estos bancos, categoria o  medio de pago");
            }

            if (promo.MaximaCantidadDeCuotas == null && promo.PorcentajeDedescuento == null)
            {
                _logger.LogError("Error en validacion de Promocion : La promoción al menos debe tener cantidad de cuotas o porcentaje de descuento");
                throw new Exception("La promoción al menos debe tener cantidad de cuotas o porcentaje de descuento");
            }

            if (promo.MaximaCantidadDeCuotas == null && promo.PorcentajeDedescuento != null)
            {
                _logger.LogError("Error en validacion de Promocion : La promoción al menos debe tener cantidad de cuotas o porcentaje de descuento");
                throw new Exception("Si tenes un porcentaje de descuento, debes agregar una cantidad de cuotas");
            }

            if (promo.PorcentajeDedescuento < 5 || promo.PorcentajeDedescuento > 80)
            {
                throw new Exception("El porcentaje de descuento esta fuera del rango permitido.");
            }

            if (promo.FechaInicio > promo.FechaFin)
            {
                throw new Exception("La fecha de inicio no puede ser mayor a la fecha fin.");
            }
        }
    }
}