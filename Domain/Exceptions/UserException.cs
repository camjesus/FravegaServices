using System;

namespace Domain.Core.Exceptions
{
    [Serializable]
    public class UserException : Exception
    {
        public UserException(string message)
            : base(message)
        { }

        public UserException(string errorMessage, int internalCode) {
            
            }
        public UserException(string message, Exception inner )
            : base(message, inner)
        { }
    }
}