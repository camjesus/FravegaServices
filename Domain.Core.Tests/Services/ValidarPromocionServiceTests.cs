using AutoFixture.Xunit2;
using Domain.Core.Data;
using Domain.Core.Services;
using FluentAssertions;
using FravegaService.Domain.Core.DTO;
using FravegaService.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Tests.Utils;
using Xunit;
using Entities = FravegaService.Models;

namespace Domain.Core.Tests.Services
{
    public class ValidarPromocionServiceTests
    {
        [Theory]
        [DefaultData]
        public async Task ValidarPromocion_Valid_ShouldNotThrowException(
            Promotion dto,
            [Frozen] Mock<IValidarPromocionService> validarPromocionServiceMock,
            [Frozen] Mock<IValidarCuotasService> validarCuotasServiceMock,
            [Frozen] Mock<IValidarExistenciaService> validarExistenciaServiceMock,
            [Frozen] Mock<IValidarPorcentajeService> validarPorcentajeServiceMock,
            [Frozen] Mock<IValidarFechasService> validarFechasServiceMock,
            ValidarPromocionService sut //system under test
            )
        {
            //arrange

            validarPromocionServiceMock
                .Setup(x => x.ValidarAsync(dto))
                .Verifiable();

            validarCuotasServiceMock
                .Setup(x => x.ValidarCuotasYPorcentaje(dto))
                .Verifiable();

              validarCuotasServiceMock
                .Setup(x => x.ValidarValorInteres(dto))
                .Verifiable();

            validarExistenciaServiceMock
                .Setup(x => x.ValidarExistencia(dto))
                .Verifiable();

            validarPorcentajeServiceMock
                .Setup(x => x.ValidarPorcentaje(dto))
                .Verifiable();

            validarFechasServiceMock
                .Setup(x => x.ValidarFechas((DateTime)dto.FechaInicio, (DateTime)dto.FechaFin))
                .Verifiable();

            //act

            Func<Task> func = async () => await sut.ValidarAsync(dto);

            //assert
            await func
                 .Should()
                 .NotThrowAsync();

            validarCuotasServiceMock
                .Verify(x => x.ValidarCuotasYPorcentaje(dto), Times.Once);

            validarCuotasServiceMock
                .Verify(x => x.ValidarValorInteres(dto), Times.Once);

            validarExistenciaServiceMock
                 .Verify(x => x.ValidarExistencia(dto), Times.Once);

            validarPorcentajeServiceMock
                 .Verify(x => x.ValidarPorcentaje(dto), Times.Once);

            validarFechasServiceMock
                 .Verify(x => x.ValidarFechas((DateTime)dto.FechaInicio, (DateTime)dto.FechaFin), Times.Once);

        }


        [Theory]
        [DefaultData]
        public async Task ValidarPromocion_ValidationThrowException_ShouldThrowException(
            Promotion dto,
            [Frozen] Mock<IValidarPromocionService> validarPromocionServiceMock,
            [Frozen] Mock<IValidarCuotasService> validarCuotasServiceMock,
            [Frozen] Mock<IValidarExistenciaService> validarExistenciaServiceMock,
            [Frozen] Mock<IValidarPorcentajeService> validarPorcentajeServiceMock,
            [Frozen] Mock<IValidarFechasService> validarFechasServiceMock,
            Exception exception,
            ValidarPromocionService sut //system under test
            )
        {
            //arrange

            validarPromocionServiceMock
                .Setup(x => x.ValidarAsync(dto))
                .Verifiable();

            validarCuotasServiceMock
                .Setup(x => x.ValidarCuotasYPorcentaje(dto))
                .Verifiable();

            validarCuotasServiceMock
              .Setup(x => x.ValidarValorInteres(dto))
              .Verifiable();

            validarExistenciaServiceMock
                .Setup(x => x.ValidarExistencia(dto))
                .Throws(exception);

            validarPorcentajeServiceMock
                .Setup(x => x.ValidarPorcentaje(dto))
                .Verifiable();

            validarFechasServiceMock
                .Setup(x => x.ValidarFechas((DateTime)dto.FechaInicio, (DateTime)dto.FechaFin))
                .Verifiable();

            //act

            Func<Task> func = async () => await sut.ValidarAsync(dto);

            //assert
            await func
                 .Should()
                 .ThrowAsync<Exception>()
                 .WithMessage(exception.Message);

            validarExistenciaServiceMock
               .Verify(x => x.ValidarExistencia(dto), Times.Once);

            validarCuotasServiceMock
                .Verify(x => x.ValidarCuotasYPorcentaje(dto), Times.Never);

            validarCuotasServiceMock
                .Verify(x => x.ValidarValorInteres(dto), Times.Never);

            validarPorcentajeServiceMock
                 .Verify(x => x.ValidarPorcentaje(dto), Times.Never);

            validarFechasServiceMock
                 .Verify(x => x.ValidarFechas((DateTime)dto.FechaInicio, (DateTime)dto.FechaFin), Times.Never);

        }
    }

}
 