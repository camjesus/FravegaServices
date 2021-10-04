namespace Domain.Core.Exceptions
{
    public class ElPorcentanjeEstaFueraDelRangoPermitidoException : UserException
    {
        public ElPorcentanjeEstaFueraDelRangoPermitidoException()
            : base("El porcentaje de descuento esta fuera del rango permitido.")
        { }
    }
}