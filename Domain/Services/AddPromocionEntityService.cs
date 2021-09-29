using Domain.Core.Data;
using FravegaService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FravegaService.Services
{
    public interface IAddPromocionEntityService
    {
        Task<Guid> AddPromocionEntity(Promotion promo);
    }

    public class AddPromocionEntityService : IAddPromocionEntityService
    {
        private readonly ILogger<Promotion> _logger;
        private readonly IPromotionRepository _promotion;
        private readonly ValidarCuotasService _validarCuotasSrv;
        private readonly ValidarExistenciaService _validarExistenciaSrv;
        private readonly ValidarPorcentajeService _validarPorcentajeSrv;


        public AddPromocionEntityService(ILogger<Promotion> logger, ValidarCuotasService validarCuotasSrv, ValidarExistenciaService validarExistenciaSrv,
            ValidarPorcentajeService validarPorcentajeSrv, IPromotionRepository promotion)
        {
            _logger = logger;
            _validarCuotasSrv = validarCuotasSrv;
            _validarExistenciaSrv = validarExistenciaSrv;
            _validarPorcentajeSrv = validarPorcentajeSrv;
            _promotion = promotion;
        }

        public async Task<Guid> AddPromocionEntity(Promotion promo)
        {
            _validarExistenciaSrv.ValidarExistencia(promo);
            _validarCuotasSrv.ValidarCuotas(promo.MaximaCantidadDeCuotas, promo.PorcentajeDedescuento);
            _validarPorcentajeSrv.ValidarPorcentaje(promo.PorcentajeDedescuento);

            Promotion promocionEntity = new Promotion(new Guid(), promo.MediosDePago, promo.Bancos, promo.CategoriasProductos, promo.MaximaCantidadDeCuotas,
                promo.ValorInteresesCuotas, promo.PorcentajeDedescuento, promo.FechaInicio, promo.FechaFin, true, DateTime.Now.Date, null);

            await _promotion.AddAsync(promocionEntity);
            return promocionEntity.Id;
        }
    }
}