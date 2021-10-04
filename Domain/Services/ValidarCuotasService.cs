using Domain.Core.Exceptions;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;

namespace FravegaService.Services
{
    public interface IValidarCuotasService
    {
        void ValidarValorInteres(Promotion promotion);
        void ValidarCuotasYPorcentaje(Promotion promotion);
    }

    public class ValidarCuotasService : IValidarCuotasService
    {
        private readonly ILogger<Promotion> _logger;

        public ValidarCuotasService(ILogger<Promotion> logger)
        {
            _logger = logger;
        }

        public void ValidarCuotasYPorcentaje(Promotion promotion)
        {
            if (promotion.MaximaCantidadDeCuotas == null && promotion.PorcentajeDedescuento == null)
            {
                _logger.LogError("Error en validacion de Promocion : La promoción al menos debe tener cantidad de cuotas o porcentaje de descuento");
                throw new CantidadDeCuotasOProcentajeDescuentoTieneQueTenerValorException();
            }

            if (promotion.MaximaCantidadDeCuotas != null && promotion.PorcentajeDedescuento != null)
            {
                throw new CantidadDeCuotasYPorcentajeAmbosNoPuedenTenerValorException();
            }
        }

        public void ValidarValorInteres(Promotion promotion)
        {
            if (promotion.MaximaCantidadDeCuotas == null && promotion.ValorInteresesCuotas != null)
            {
                _logger.LogError("Error en validacion de Promocion : La promoción al menos debe tener cantidad de cuotas o porcentaje de descuento");
                throw new AlAgregarValorDeIntesDebeTenerCantidadDeCuotasException();
            }
        }

        
    }
}