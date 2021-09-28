using AutoMapper;
using Domain.Core.Data;
using FravegaService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FravegaService.Services
{
    public interface IGetPromocionByIdService
    {
        Task<Promotion> GetPromocionById(Guid id);
    }

    public class GetPromocionByIdService : IGetPromocionByIdService
    {
        private readonly ILogger<Promotion> _logger;
        private readonly IPromotionRepository _promotion;

        public GetPromocionByIdService(ILogger<Promotion> logger, IPromotionRepository promotion)
        {
            _logger = logger;
            _promotion = promotion;
        }

        public async Task<Promotion> GetPromocionById(Guid id)
        {
            _logger.LogInformation("Se consulta promocion por Id" + id);
            return await _promotion.FindOne(id);
        }
    }
}