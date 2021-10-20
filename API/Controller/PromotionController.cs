using Domain.Core.Services;
using FravegaService.Domain.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities = FravegaService.Models;

namespace API.Controller
{
    [Route("promotion")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IGetPromocionByIdService _getPromocionByIdService;
        private readonly IGetPromocionesVigentesService _getPromocionesVigentesService;
        private readonly IGetPromocionesService _getPromocionesService;
        private readonly IAddPromocionEntityService _addPromocionEntityService;
        private readonly IUpdatePromocionService _updatePromocionService;
        private readonly IUpdateVigenciaService _updateVigenciaService;
        private readonly IDeletePromotionService _deletePromotionService;

        public PromotionController(
            IGetPromocionByIdService getPromocionByIdService,
            IAddPromocionEntityService addPromocionEntityService,
            IGetPromocionesVigentesService getPromocionesVigentesService,
            IGetPromocionesService getPromocionesService,
            IUpdatePromocionService updatePromocionService,
            IUpdateVigenciaService updateVigenciaService,
            IDeletePromotionService deletePromotionService)
        {
            _getPromocionByIdService = getPromocionByIdService ?? throw new ArgumentNullException(nameof(getPromocionByIdService));
            _addPromocionEntityService = addPromocionEntityService ?? throw new ArgumentNullException(nameof(addPromocionEntityService));
            _getPromocionesVigentesService = getPromocionesVigentesService ?? throw new ArgumentNullException(nameof(getPromocionesVigentesService));
            _getPromocionesService = getPromocionesService ?? throw new ArgumentNullException(nameof(getPromocionesService));
            _updatePromocionService = updatePromocionService ?? throw new ArgumentNullException(nameof(updatePromocionService));
            _updateVigenciaService = updateVigenciaService ?? throw new ArgumentNullException(nameof(updateVigenciaService));
            _deletePromotionService = deletePromotionService ?? throw new ArgumentNullException(nameof(deletePromotionService));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Entities.Promotion> GetByIdAsync([FromRoute] Guid id)
            => await _getPromocionByIdService.GetPromocionById(id);

        [HttpGet]
        public async Task<IEnumerable<Entities.Promotion>> GetAllAsync()
        {
            return await _getPromocionesService.GetPromociones();
        }

        [HttpGet]
        [Route("{date}/current")]
        [Route("current")]  ///promotions/current?date={date}
        public async Task<IEnumerable<CurrentPromotion>> GetAllVigentesAsync([FromQuery] DateTime date)
        {
            return await _getPromocionesVigentesService.GetPromocionesVigentesAsync(date);
        }

        [HttpGet]
        [Route("{banks}/{categories}/{paymentMethod}/current")]
        public async Task<IEnumerable<CurrentPromotion>> GetAllVigentesFilterAsync([FromRoute] IEnumerable<string> banco, [FromRoute] IEnumerable<string> medioDePago, [FromRoute] IEnumerable<string> categorias)
        {
            return await _getPromocionesVigentesService.GetPromocionesFilterVigentesAsync(banco, medioDePago, categorias);
        }

        [HttpPost]
        public async Task<Guid> CreateAsync([FromBody] Promotion promotion)
        {
            return await _addPromocionEntityService.AddPromocionEntity(promotion);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<Guid> UpdateAsync([FromRoute] Guid id, [FromBody] PromotionUpd promotion)
        {
            promotion.Id = id;

            return await _updatePromocionService.UpdatePromocion(promotion);
        }

        [HttpPost]
        [Route("{id}/updateCurrent")]
        public async Task<Guid> UpdateVigenciaAsync([FromRoute] Guid id, [FromBody] UpdCurrentPromotion promotion)
        {
            promotion.Id = id;

            return await _updateVigenciaService.UpdateVigencia(promotion.Id, promotion.FechaInicio, promotion.FechaFin);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<Guid> DeleteAsync([FromRoute] Guid id)
        {
            return await _deletePromotionService.DeletePromotion(id);
        }
    }
}