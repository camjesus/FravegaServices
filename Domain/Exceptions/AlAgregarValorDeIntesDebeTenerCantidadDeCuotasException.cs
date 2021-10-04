namespace Domain.Core.Exceptions
{
    public class AlAgregarValorDeIntesDebeTenerCantidadDeCuotasException : UserException
    {
        public AlAgregarValorDeIntesDebeTenerCantidadDeCuotasException()
            : base("Si tenes un interes, debes agregar una cantidad de cuotas")
        { }
    }
}