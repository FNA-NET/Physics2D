using Physics2D.Collision.ContactSystem;
using Microsoft.Xna.Framework;

namespace Physics2D.Collision.Narrowphase
{
    /// <summary>Used for computing contact manifolds.</summary>
    internal struct ClipVertex
    {
        public ContactId Id;
        public Vector2 V;
    }
}