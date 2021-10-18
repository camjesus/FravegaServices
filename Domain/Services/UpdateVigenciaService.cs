using Entities = FravegaService.Models;
using Domain.Core.Data;
using Domain.Core.Services;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Domain.Core.Services
{
    public interface IUpdateVigenciaService
    {
        Task<Guid> UpdateVigencia(Guid id, DateTime fechaInicio, DateTime fechaFin);
    }

    public class UpdateVigenciaService : IUpdateVigenciaService
    {
        private readonly ValidarFechasService _validarFechas;
        private readonly IPromotionRepository _promotion;

        public UpdateVigenciaService(ValidarFechasService validarFechas, IPromotionRepository promotion)
        {
            _validarFechas = validarFechas ?? throw new ArgumentNullException(nameof(validarFechas));
            _promotion = promotion ?? throw new ArgumentNullException(nameof(promotion));
        }

        public async Task<Guid> UpdateVigencia(Guid id, DateTime fechaInicio, DateTime fechaFin)
        {
            var promotionEntity = await _promotion.FindOneAsync(id);
            _validarFechas.ValidarFechas(fechaInicio, fechaFin);

            promotionEntity.ChangeVigencia(fechaInicio, fechaFin);

            await _promotion.UpdateAsync(promotionEntity);
            return id;
        }
    }
}