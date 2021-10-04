namespace Domain.Core.Exceptions
{
    public class YaExistePromocionException : UserException
    {
        public YaExistePromocionException()
            : base("Ya existe una Promocion para estos bancos, categoria o  medio de pago")
        { }
    }
}