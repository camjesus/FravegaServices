using System;
using System.Collections.Generic;
using System.Linq;

namespace FravegaService.Domain.Core.DTO
{
    public class Promotion : BaseEntity
    {
        public IEnumerable<string> MediosDePago { get; set; }
        public IEnumerable<string> Bancos { get; set; }
        public IEnumerable<string> CategoriasProductos { get; set; }
        public int? MaximaCantidadDeCuotas { get; set; }
        public decimal? ValorInteresesCuotas { get; set; }
        public decimal? PorcentajeDedescuento { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
