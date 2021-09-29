using Domain.Core.Data;
using FravegaService.Models;
using Microsoft.Extensions.Logging;
using System;

namespace FravegaService.Services
{
    public interface IValidarPorcentajeService
    {
        void ValidarPorcentaje(decimal? porcentaje);
    }

    public class ValidarPorcentajeService : IValidarPorcentajeService
    {
        private readonly ILogger<Promotion> _logger;

        public ValidarPorcentajeService(ILogger<Promotion> logger)
        {
            _logger = logger;
        }

        public void ValidarPorcentaje(decimal? porcentaje)
        {
            _logger.LogInformation("Se valida el porcentaje");
            if (porcentaje < 5 || porcentaje > 80)
            {
                _logger.LogError("El porcentaje de descuento esta fuera del rango permitido. porcentaje:" + porcentaje);
                throw new Exception("El porcentaje de descuento esta fuera del rango permitido.");
            }
        }
    }
}