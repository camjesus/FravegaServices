using Domain.Core.Exceptions;
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
    public class ValidarPorcentajeServiceTests
    {
        [Theory]
        [InlineDefaultData(5)]
        [InlineDefaultData(80)]
        public void ValidarPorcentaje_Valid_ShouldNotThrowException(
            int? porcentajeDeDescuento,
            Promotion promotion,
            ValidarPorcentajeService sut)
        {
            //arrange

            promotion.PorcentajeDedescuento = porcentajeDeDescuento;

            //act

            Action act = () => sut.ValidarPorcentaje(promotion); 

            //assert

            act.Should().NotThrow();
        }

        [Theory]
        [InlineDefaultData(4)]
        [InlineDefaultData(81)]
        public void ValidarPorcentaje_ValidFueraDeRango_ShouldThrowElPorcentanjeEstaFueraDelRangoPermitidoException(
           int? porcentajeDeDescuento,
            Promotion promotion,
            ValidarPorcentajeService sut)
        {
            //arrange
           
            promotion.PorcentajeDedescuento = porcentajeDeDescuento;

            //act

            Action act = () => sut.ValidarPorcentaje(promotion);

            //assert
            
            act.Should().Throw<ElPorcentanjeEstaFueraDelRangoPermitidoException>();
        }
    }

}
