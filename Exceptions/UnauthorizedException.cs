using System;

namespace NeComPlus.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message)
            : base(message)
        {
        }

        public UnauthorizedException()
        {
        }
    }
}
