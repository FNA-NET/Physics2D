using Physics2D.Shared.Optimization;

namespace Physics2D.UnitTests.Code
{
    internal sealed class PoolObject : IPoolable<PoolObject>
    {
        public PoolObject()
        {
            IsNew = true;
        }

        public bool IsNew { get; private set; }

        public void Dispose() { }

        public void Reset()
        {
            IsNew = false;
        }
    }
}