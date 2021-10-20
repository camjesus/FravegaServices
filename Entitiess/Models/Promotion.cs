using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FravegaService.Models
{
    public class Promotion
    {

        [Key]
        public Guid Id { get; private set; }
        public IEnumerable<string> MediosDePago { get; private set; }
        public IEnumerable<string> Bancos { get; private set; }
        public IEnumerable<string> CategoriasProductos { get; private set; }
        public int? MaximaCantidadDeCuotas { get; private set; }
        public decimal? ValorInteresesCuotas { get; private set; }
        public decimal? PorcentajeDedescuento { get; private set; }
        public DateTime? FechaInicio { get; private set; }
        public DateTime? FechaFin { get; private set; }
        public bool Activo { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaModificacion { get; private set; }

        public string Hash { get; private set; }

        public Promotion(IEnumerable<string> mediosDePago, IEnumerable<string> bancos, IEnumerable<string> categoriasProductos, int? maximaCantidadDeCuotas,
           decimal? valorInteresesCuotas, decimal? porcentajeDedescuento, DateTime? fechaInicio, DateTime? fechaFin)
        {
            Id = Guid.NewGuid();
            MediosDePago = mediosDePago;
            Bancos = bancos;
            CategoriasProductos = categoriasProductos;
            MaximaCantidadDeCuotas = maximaCantidadDeCuotas;
            ValorInteresesCuotas = valorInteresesCuotas;
            PorcentajeDedescuento = porcentajeDedescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Activo = true;
            FechaCreacion = DateTime.Now.Date;

            SetHash();
        }

        //Update
        public void UpdatePromotion(IEnumerable<string> mediosDePago, IEnumerable<string> bancos, IEnumerable<string> categoriasProductos, int? maximaCantidadDeCuotas,
          decimal? valorInteresesCuotas, decimal? porcentajeDedescuento, DateTime? fechaInicio, DateTime? fechaFin)
        {
            MediosDePago = mediosDePago;
            Bancos = bancos;
            CategoriasProductos = categoriasProductos;
            MaximaCantidadDeCuotas = maximaCantidadDeCuotas;
            ValorInteresesCuotas = valorInteresesCuotas;
            PorcentajeDedescuento = porcentajeDedescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;

            Update();
        }

        public void ChangeVigencia(DateTime? fechaInicio, DateTime? fechaFin)
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;

            Update();
        }

        public void Delete()
        {
            if (Activo == false)
                throw new ArgumentException("promoción ya eliminada");

            Activo = false;

            Update();
        }

        private void Update()
        {
            FechaModificacion = DateTime.Now.Date;
            SetHash();
        }

        private void SetHash()
        {
            Hash = $"{string.Join(",", MediosDePago)}{string.Join(",", Bancos)}{string.Join(",", CategoriasProductos)}{MaximaCantidadDeCuotas}{ValorInteresesCuotas}{PorcentajeDedescuento}{FechaInicio}{FechaFin}{Activo}";
        }
    }
}
