using System;

namespace Physics2D.Shared.Optimization
{
    public interface IPoolable<T> : IDisposable where T : IPoolable<T>
    {
        void Reset();
    }
}