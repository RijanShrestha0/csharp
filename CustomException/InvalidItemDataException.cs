using System;

namespace Library.CustomException
{
    public class InvalidItemDataException : Exception
    {
        public InvalidItemDataException(string message) : base(message) { }
    }
}
