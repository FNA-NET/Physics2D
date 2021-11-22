using Physics2D.Collision.Narrowphase;
using Microsoft.Xna.Framework;

namespace Physics2D.Dynamics.Solver
{
    public sealed class ContactPositionConstraint
    {
        public int IndexA;
        public int IndexB;
        public float InvIA, InvIB;
        public float InvMassA, InvMassB;
        public Vector2 LocalCenterA, LocalCenterB;
        public Vector2 LocalNormal;
        public Vector2 LocalPoint;
        public Vector2[] LocalPoints = new Vector2[Settings.MaxManifoldPoints];
        public int PointCount;
        public float RadiusA, RadiusB;
        public ManifoldType Type;
    }
}