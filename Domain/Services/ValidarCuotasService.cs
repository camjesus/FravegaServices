using Domain.Core.Exceptions;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;

namespace FravegaService.Services
{
    public interface IValidarCuotasService
    {
        void ValidarCuotas(Promotion promotion);
    }

    public class ValidarCuotasService : IValidarCuotasService
    {
        private readonly ILogger<Promotion> _logger;

        public ValidarCuotasService(ILogger<Promotion> logger)
        {
            _logger = logger;
        }

        public void ValidarCuotas(Promotion promotion)
        {
            _logger.LogInformation("Valido cuotas");

            if (promotion.MaximaCantidadDeCuotas == null && promotion.PorcentajeDedescuento == null)
            {
                _logger.LogError("Error en validacion de Promocion : La promoción al menos debe tener cantidad de cuotas o porcentaje de descuento");
                throw new CantidadDeCuotasOProcentajeDescuentoTieneQueTenerValorException();
            }

            if (promotion.MaximaCantidadDeCuotas != null && promotion.PorcentajeDedescuento != null)
            {
                throw new Exception("no se pueden los dos juntos");
            }

            if (promotion.MaximaCantidadDeCuotas == null && promotion.ValorInteresesCuotas != null)
            {
                _logger.LogError("Error en validacion de Promocion : La promoción al menos debe tener cantidad de cuotas o porcentaje de descuento");
                throw new Exception("Si tenes un interes, debes agregar una cantidad de cuotas");
            }
        }
    }
}