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
    public class ValidarFechasServiceTests
    {
        [Theory]
        [DefaultData]
        public void ValidarFechas_Valid_ShouldNotThrowException(
            ValidarFechasService sut)
        {
            //arrange

            DateTime fechaInicio = new DateTime(2021,01,1);
            DateTime fechaFin = new DateTime(2021, 01, 10);

            //act

            Action act = () => sut.ValidarFechas(fechaInicio, fechaFin); 

            //assert

            act.Should().NotThrow();
        }

        [Theory]
        [DefaultData]
        public void ValidarFechas_Valid_ShouldThrowFechaInicioMayorFechaFinException(
                ValidarFechasService sut)
        {
            //arrange

            DateTime fechaInicio = new DateTime(2021, 01, 10);
            DateTime fechaFin = new DateTime(2021, 01, 1);

            //act

            Action act = () => sut.ValidarFechas(fechaInicio, fechaFin);

            //assert

            act.Should().Throw<FechaInicioMayorFechaFinException>();
        }
    }

}
