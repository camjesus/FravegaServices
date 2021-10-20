namespace Domain.Core.Exceptions
{
    public class DuplicateEntityException : UserException
    {
        public DuplicateEntityException(string entity)
            : base($"Duplicate entry: {entity}")
        { }
    }

    public class DuplicateEntityException<T> : DuplicateEntityException
    {
        public DuplicateEntityException()
            : base(nameof(T))
        { }
    }
}