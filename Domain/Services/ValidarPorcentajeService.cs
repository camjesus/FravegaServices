using Domain.Core.Data;
using Domain.Core.Exceptions;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;

namespace Domain.Core.Services
{
    public interface IValidarPorcentajeService
    {
        void ValidarPorcentaje(Promotion promotion);
    }

    public class ValidarPorcentajeService : IValidarPorcentajeService
    {
        //private readonly ILogger<Promotion> _logger;

        public ValidarPorcentajeService()
        {
            //_logger = logger;
        }

        public void ValidarPorcentaje(Promotion promotion)
        {
            //_logger.LogInformation("Se valida el porcentaje");
            if (promotion.PorcentajeDedescuento < 5 || promotion.PorcentajeDedescuento > 80)
            {
                //_logger.LogError("El porcentaje de descuento esta fuera del rango permitido. porcentaje:" + promotion.PorcentajeDedescuento);
                throw new ElPorcentanjeEstaFueraDelRangoPermitidoException();
            }
        }
    }
}