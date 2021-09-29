using FravegaService.Domain.Core.DTO;
using FravegaService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Services
{
    public interface IValidarCrearPromocionService
    {
        Task ValidarAsync(Promotion promotion);
    }

    public class ValidarCrearPromocionService : IValidarCrearPromocionService
    {
        private readonly IValidarCuotasService _validarCuotasSrv;
        private readonly IValidarExistenciaService _validarExistenciaSrv;
        private readonly IValidarPorcentajeService _validarPorcentajeSrv;


        public ValidarCrearPromocionService(
            IValidarCuotasService validarCuotasSrv,
            IValidarExistenciaService validarExistenciaSrv,
            IValidarPorcentajeService validarPorcentajeSrv)
        {
            _validarCuotasSrv = validarCuotasSrv ?? throw new ArgumentNullException(nameof(validarCuotasSrv));
            _validarExistenciaSrv = validarExistenciaSrv ?? throw new ArgumentNullException(nameof(validarExistenciaSrv));
            _validarPorcentajeSrv = validarPorcentajeSrv ?? throw new ArgumentNullException(nameof(validarPorcentajeSrv));
        }

        public async Task ValidarAsync(Promotion promotion)
        {
            _validarExistenciaSrv.ValidarExistencia(promotion);
            _validarCuotasSrv.ValidarCuotas(promotion);
            _validarPorcentajeSrv.ValidarPorcentaje(promotion.PorcentajeDedescuento);
        }
    }
}
