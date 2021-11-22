using System;

namespace Physics2D.Shared.Contracts
{
    public class RequiredException : Exception
    {
        public RequiredException(string message) : base(message) { }
    }
}