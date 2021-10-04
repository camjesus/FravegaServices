namespace Domain.Core.Exceptions
{
    public class CantidadDeCuotasYPorcentajeAmbosNoPuedenTenerValorException : UserException
    {
        public CantidadDeCuotasYPorcentajeAmbosNoPuedenTenerValorException()
            : base("Ambos no pueden tener valor")
        { }
    }
}