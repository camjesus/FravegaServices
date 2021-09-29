namespace Domain.Core.Exceptions
{
    public class CantidadDeCuotasOProcentajeDescuentoTieneQueTenerValorException : UserException
    {
        public CantidadDeCuotasOProcentajeDescuentoTieneQueTenerValorException()
            : base("La promoción al menos debe tener cantidad de cuotas o porcentaje de descuento")
        { }
    }
}