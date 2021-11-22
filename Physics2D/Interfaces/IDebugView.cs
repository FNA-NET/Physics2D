using Physics2D.Collision.Shapes;
using Physics2D.Dynamics.Joints;
using Physics2D.Shared;
using Microsoft.Xna.Framework;

namespace Physics2D.Interfaces
{
    public interface IDebugView
    {
        void DrawJoint(Joint joint);
        void DrawShape(Shape shape, ref Transform transform, Color color);
    }
}