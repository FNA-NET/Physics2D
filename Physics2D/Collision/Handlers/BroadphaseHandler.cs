using Physics2D.Dynamics;

namespace Physics2D.Collision.Handlers
{
    public delegate void BroadphaseHandler(ref FixtureProxy proxyA, ref FixtureProxy proxyB);
}