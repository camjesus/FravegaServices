using Domain.Core.Data;
using FravegaService.Models;
using Microsoft.Extensions.Logging;
using System;

namespace FravegaService.Services
{
    public interface IValidarCuotasService
    {
        void ValidarCuotas(int? cuotas, decimal? porcentaje);
    }

    public class ValidarCuotasService : IValidarCuotasService
    {
        private readonly ILogger<Promotion> _logger;

        public ValidarCuotasService(ILogger<Promotion> logger)
        {
            _logger = logger;
        }

        public void ValidarCuotas(int? cuotas, decimal? porcentaje)
        {
            _logger.LogInformation("Valido cuotas");

            if (cuotas == null && porcentaje == null)
            {
                _logger.LogError("Error en validacion de Promocion : La promoción al menos debe tener cantidad de cuotas o porcentaje de descuento");
                throw new Exception("La promoción al menos debe tener cantidad de cuotas o porcentaje de descuento");
            }

            if (cuotas == null && porcentaje != null)
            {
                _logger.LogError("Error en validacion de Promocion : La promoción al menos debe tener cantidad de cuotas o porcentaje de descuento");
                throw new Exception("Si tenes un porcentaje de descuento, debes agregar una cantidad de cuotas");
            }

          
        }
    }
}