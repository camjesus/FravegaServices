namespace Domain.Core.Exceptions
{
    public class PromotionAlreadyDeletedException : UserException
    {
        public PromotionAlreadyDeletedException()
            : base("promocion ya eliminada")
        { }
    }
}