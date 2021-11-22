using Physics2D.Collision.Shapes;
using Physics2D.Shared;

namespace Physics2D.Definitions.Shapes
{
    public sealed class PolygonShapeDef : ShapeDef
    {
        public PolygonShapeDef() : base(ShapeType.Polygon)
        {
            SetDefaults();
        }

        public Vertices Vertices { get; set; }

        public override void SetDefaults()
        {
            Vertices = null;
            base.SetDefaults();
        }
    }
}