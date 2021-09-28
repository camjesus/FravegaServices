using System;
using System.Collections.Generic;

namespace FravegaService.Models
{
    public class CurrentPromotion
    {
      
        public Guid Id { get;  set; }
        public IEnumerable<string> MediosDePago { get;  set; }
        public IEnumerable<string> Bancos { get;  set; }
        public IEnumerable<string> CategoriasProductos { get;  set; }
        public int? MaximaCantidadDeCuotas { get;  set; }
        public decimal? ValorInteresesCuotas { get;  set; }
        public decimal? PorcentajeDedescuento { get;  set; }
     
    }

   
}
