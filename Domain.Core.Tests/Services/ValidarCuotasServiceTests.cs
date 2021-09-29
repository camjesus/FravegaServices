using FluentAssertions;
using FravegaService.Domain.Core.DTO;
using FravegaService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Utils;
using Xunit;

namespace Domain.Core.Tests.Services
{
    public class ValidarCuotasServiceTests
    {
        //dejo como ejemplo, pero abajo hay una forma mejor
        [Theory]
        [DefaultData]
        public void ValidarCuotas_Valid_ShouldNotThrowException(
            Promotion promotion,
            ValidarCuotasService sut)
        {
            //arrange

            promotion.MaximaCantidadDeCuotas = null;
            promotion.ValorInteresesCuotas = null;
            promotion.PorcentajeDedescuento = 1;

            //act

            Action act = () => sut.ValidarCuotas(promotion);

            //assert

            act.Should().NotThrow();
        }


        [Theory]
        [DefaultData]
        public void ValidarCuotas_Valid2_ShouldNotThrowException(
            Promotion promotion,
            ValidarCuotasService sut)
        {
            //arrange

            promotion.MaximaCantidadDeCuotas = 1;
            promotion.ValorInteresesCuotas = null;
            promotion.PorcentajeDedescuento = null;

            //act

            Action act = () => sut.ValidarCuotas(promotion);

            //assert

            act.Should().NotThrow();
        }

        [Theory]
        [DefaultData]
        public void ValidarCuotas_Valid3_ShouldNotThrowException(
            Promotion promotion,
            ValidarCuotasService sut)
        {
            //arrange

            promotion.MaximaCantidadDeCuotas = 1;
            promotion.ValorInteresesCuotas = 1;
            promotion.PorcentajeDedescuento = null;

            //act

            Action act = () => sut.ValidarCuotas(promotion);

            //assert

            act.Should().NotThrow();
        }
    }
}
