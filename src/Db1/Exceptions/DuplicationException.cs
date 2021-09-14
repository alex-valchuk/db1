using System;

namespace Db1.Exceptions
{
    public class DuplicationException : Exception
    {
        public DuplicationException(string message)
            : base(message)
        {
        }
    }
}