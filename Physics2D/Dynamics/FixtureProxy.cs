using Physics2D.Collision.Broadphase;
using Physics2D.Shared;

namespace Physics2D.Dynamics
{
    /// <summary>This proxy is used internally to connect fixtures to the broad-phase.</summary>
    public struct FixtureProxy
    {
        public AABB AABB;
        public int ChildIndex;
        public Fixture Fixture;
        public int ProxyId;
    }
}