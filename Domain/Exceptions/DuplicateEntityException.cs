namespace Domain.Core.Exceptions
{
    public class DuplicateEntityException<T> : UserException
    {
        public T OldEntity { get; }

        public DuplicateEntityException(T oldEntity)
            : base("Duplicate entry")
        {
            OldEntity = oldEntity;
        }
    }
}