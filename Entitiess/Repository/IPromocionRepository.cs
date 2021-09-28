using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FravegaService.Models;

namespace FravegaService.Ropositories
{
    public interface IPromocionRepository : IRepository<Promocion>
    {
        //Task<Promocion> GetPromocionesVigentes();
    }
}
