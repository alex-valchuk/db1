using System;

namespace Db1.Exceptions
{
    public class NonExistingResourceException : Exception
    {
        public NonExistingResourceException(string message)
            : base(message)
        {
        }   
    }
}