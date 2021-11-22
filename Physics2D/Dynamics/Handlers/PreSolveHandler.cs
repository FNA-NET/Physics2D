using Physics2D.Collision.ContactSystem;
using Physics2D.Collision.Narrowphase;

namespace Physics2D.Dynamics.Handlers
{
    public delegate void PreSolveHandler(Contact contact, ref Manifold oldManifold);
}