using System;

namespace Domain.Core.Exceptions
{
    public class EntityNotFoundException<T> : UserException
    {
        public T Entity { get; }

        public Guid Id { get; }

        public EntityNotFoundException(Guid id)
            : base("Not Found Entity")
        {
            Id = id;
        }
    }
}