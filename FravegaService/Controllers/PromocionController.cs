using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using FravegaService.Services;
using FravegaService.Services.DTO;
using FravegaService.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace FravegaService.Controller
{
    [Route("promociones")]
    [ApiController]
    public class PromocionController : ControllerBase
    {
         private readonly AddPromocionEntityService _addPromocionService;
         public PromocionController(AddPromocionEntityService addPromocionService)
         {
             this._addPromocionService = addPromocionService;
         }
    
     [HttpPost("add/")]
     public async Task<Guid> AddAsync(Promocion promocionDTO)
     {
            Console.WriteLine("UP");
            return await _addPromocionService.AddPromocionEntity(promocionDTO);
     }
}
}
