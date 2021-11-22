using System;

namespace Physics2D.Shared.Contracts
{
    public class EnsuresException : Exception
    {
        public EnsuresException(string message) : base(message) { }
    }
}