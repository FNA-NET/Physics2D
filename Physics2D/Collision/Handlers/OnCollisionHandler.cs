using Physics2D.Collision.ContactSystem;
using Physics2D.Dynamics;

namespace Physics2D.Collision.Handlers
{
    public delegate void OnCollisionHandler(Fixture fixtureA, Fixture fixtureB, Contact contact);
}