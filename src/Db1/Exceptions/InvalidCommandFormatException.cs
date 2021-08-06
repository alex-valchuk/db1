using System;

namespace Db1.Exceptions
{
    public class InvalidCommandFormatException : Exception
    {
        public InvalidCommandFormatException(string message)
            : base(message)
        {
        }
    }
}