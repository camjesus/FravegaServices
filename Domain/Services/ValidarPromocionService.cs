using FravegaService.Domain.Core.DTO;
using Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Services
{
    public interface IValidarPromocionService
    {
        Task ValidarAsync(Promotion promotion);
    }

    public class ValidarPromocionService : IValidarPromocionService
    {
        private readonly IValidarCuotasService _validarCuotasSrv;
        private readonly IValidarExistenciaService _validarExistenciaSrv;
        private readonly IValidarPorcentajeService _validarPorcentajeSrv;
        private readonly IValidarFechasService _validarFechasSrv;


        public ValidarPromocionService(
            IValidarCuotasService validarCuotasSrv,
            IValidarExistenciaService validarExistenciaSrv,
            IValidarPorcentajeService validarPorcentajeSrv,
            IValidarFechasService validarFechasSrv)
        {
            _validarCuotasSrv = validarCuotasSrv ?? throw new ArgumentNullException(nameof(validarCuotasSrv));
            _validarExistenciaSrv = validarExistenciaSrv ?? throw new ArgumentNullException(nameof(validarExistenciaSrv));
            _validarPorcentajeSrv = validarPorcentajeSrv ?? throw new ArgumentNullException(nameof(validarPorcentajeSrv));
            _validarFechasSrv = validarFechasSrv ?? throw new ArgumentNullException(nameof(validarFechasSrv));
        }

        public async Task ValidarAsync(Promotion promotion)
        {
                await _validarExistenciaSrv.ValidarExistencia(promotion);
                _validarCuotasSrv.ValidarCuotasYPorcentaje(promotion);
                _validarCuotasSrv.ValidarValorInteres(promotion);
                _validarPorcentajeSrv.ValidarPorcentaje(promotion);
                _validarFechasSrv.ValidarFechas((DateTime)promotion.FechaInicio, (DateTime)promotion.FechaFin);
        }
    }
}
