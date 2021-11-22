using Physics2D.Collision.ContactSystem;
using Physics2D.Dynamics;
using Physics2D.Dynamics.Solver;

namespace Physics2D.Collision.Handlers
{
    public delegate void AfterCollisionHandler(Fixture fixtureA, Fixture fixtureB, Contact contact, ContactVelocityConstraint impulse);
}