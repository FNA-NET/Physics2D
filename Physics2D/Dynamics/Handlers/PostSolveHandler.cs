using Physics2D.Collision.ContactSystem;
using Physics2D.Dynamics.Solver;

namespace Physics2D.Dynamics.Handlers
{
    public delegate void PostSolveHandler(Contact contact, ContactVelocityConstraint impulse);
}