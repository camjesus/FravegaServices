using Domain.Core.Data;
using FravegaService.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FravegaService.Services
{
    public interface IGetPromocionesService
    {
        Task<IEnumerable<Promotion>> GetPromociones();
    }

    public class GetPromocionesService : IGetPromocionesService
    {
        private readonly ILogger<Promotion> _logger;
        private readonly IPromotionRepository _promotion;

        public GetPromocionesService(ILogger<Promotion> logger, IPromotionRepository promotion)
        {
            _logger = logger;
            _promotion = promotion;
        }

        public async Task<IEnumerable<Promotion>> GetPromociones()
        {
            _logger.LogInformation("Obtengo todas las promociones");
            return await _promotion.GetAllAsync();
        }
    }
}