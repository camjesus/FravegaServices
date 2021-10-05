using AutoFixture.Xunit2;
using Domain.Core.Data;
using Domain.Core.Services;
using FluentAssertions;
using FravegaService.Models;
using FravegaService.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Tests.Utils;
using Xunit;
using Entities = FravegaService.Models;

namespace Domain.Core.Tests.Models
{
    public class PromotionTests
    {
        [Theory]
        [DefaultData]
        public void PromotionEntity_Valid_ShouldCreateEntity(
            Promotion dto)
        {
            var promotion = new Entities.Promotion(dto.MediosDePago, dto.Bancos, dto.CategoriasProductos, dto.MaximaCantidadDeCuotas,
                 dto.ValorInteresesCuotas, dto.PorcentajeDedescuento, dto.FechaInicio, dto.FechaFin);

            promotion.Id.Should().NotBeEmpty();
            promotion.MediosDePago.Should().BeEquivalentTo(dto.MediosDePago);
            promotion.Bancos.Should().BeEquivalentTo(dto.Bancos);
            promotion.CategoriasProductos.Should().BeEquivalentTo(dto.CategoriasProductos);
            promotion.MaximaCantidadDeCuotas.Should().Be(dto.MaximaCantidadDeCuotas);
            promotion.ValorInteresesCuotas.Should().Be(dto.ValorInteresesCuotas);
            promotion.PorcentajeDedescuento.Should().Be(dto.PorcentajeDedescuento);
            promotion.FechaInicio.Should().Be(dto.FechaInicio);
            promotion.FechaFin.Should().Be(dto.FechaFin);
            promotion.Activo.Should().BeTrue();
            promotion.FechaModificacion.Should().BeNull();
            promotion.FechaCreacion.Should().Be(DateTime.Now.Date);
        }

        [Theory]
        [DefaultData]
        public void UpdateEntity_Valid_ShouldChangePromotion(
            Entities.Promotion promotion,
            Promotion dto
            )
        {

            promotion.UpdatePromotion(dto.MediosDePago, dto.Bancos, dto.CategoriasProductos, dto.MaximaCantidadDeCuotas,
                 dto.ValorInteresesCuotas, dto.PorcentajeDedescuento, dto.FechaInicio, dto.FechaFin);

            //promotion.Id.Should().Be(promotion.Id);
            promotion.MediosDePago.Should().BeEquivalentTo(dto.MediosDePago);
            promotion.Bancos.Should().BeEquivalentTo(dto.Bancos);
            promotion.CategoriasProductos.Should().BeEquivalentTo(dto.CategoriasProductos);
            promotion.MaximaCantidadDeCuotas.Should().Be(dto.MaximaCantidadDeCuotas);
            promotion.ValorInteresesCuotas.Should().Be(dto.ValorInteresesCuotas);
            promotion.PorcentajeDedescuento.Should().Be(dto.PorcentajeDedescuento);
            promotion.FechaInicio.Should().Be(dto.FechaInicio);
            promotion.FechaFin.Should().Be(dto.FechaFin);
            promotion.Activo.Should().BeTrue();
            promotion.FechaModificacion.Should().NotBeNull()
        }

        [Theory]
        [DefaultData]
        public void DeleteEntity_Valid_ShouldChangeActivoFalse(
            Entities.Promotion promotion
            )
        {
            promotion.Delete();
           
            promotion.Activo.Should().BeFalse();
            promotion.FechaModificacion.Should().Be(DateTime.Now.Date);
        }

        [Theory]
        [DefaultData]
        public void ChangeVigenciaEntity_Valid_ShouldChangeVigencia(
           Entities.Promotion promotion
           )
        {
            DateTime fechaInicio = new DateTime(2021, 2, 10);
            DateTime fechaFin = new DateTime(2021, 2, 20);

            promotion.ChangeVigencia(fechaInicio, fechaFin);

            promotion.FechaInicio.Should().Be(fechaInicio);
            promotion.FechaFin.Should().Be(fechaFin);
            promotion.FechaModificacion.Should().Be(DateTime.Now.Date);
        }
    }
}
