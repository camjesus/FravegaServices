using System;
using System.Collections.Generic;
using System.Linq;

namespace FravegaService.Domain.Core.DTO
{
    public class UpdCurrentPromotion
    {
        public Guid Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
