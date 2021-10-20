using Domain.Core.Data;
using Domain.Core.Exceptions;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;

namespace Domain.Core.Services
{
    public interface IValidarFechasService
    {
        void ValidarFechas(DateTime fechaInicio, DateTime fechaFin);
    }

    public class ValidarFechasService : IValidarFechasService
    {
        //private readonly ILogger<Promotion> _logger;

        public ValidarFechasService()
        {
            //_logger = logger;
        }

        public void ValidarFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            //_logger.LogInformation("Se validan las fechas");
            if (fechaInicio > fechaFin)
            {
                //_logger.LogError("La fecha de inicio no puede se mayor a fecha fin");
                throw new FechaInicioMayorFechaFinException();
            }
        }
    }
}