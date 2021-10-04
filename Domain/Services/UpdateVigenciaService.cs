using Entities = FravegaService.Models;
using Domain.Core.Data;
using Domain.Core.Services;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FravegaService.Services
{
    public interface IUpdateVigenciaService
    {
        Task<Guid> UpdateVigencia(Guid id, DateTime fechaInicio, DateTime fechaFin);
    }

    public class UpdateVigenciaService : IUpdateVigenciaService
    {
        private readonly ILogger<Promotion> _logger;
        private readonly ValidarFechasService _validarFechas;
        private readonly IPromotionRepository _promotion;

        public UpdateVigenciaService(ILogger<Promotion> logger, ValidarFechasService validarFechas, IPromotionRepository promotion)
        {
            _logger = logger;
            _validarFechas = validarFechas;
            _promotion = promotion;
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