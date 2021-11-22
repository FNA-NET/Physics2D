using Physics2D.Collision.Shapes;
using Physics2D.Shared;
using Physics2D.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics2D.UnitTests.Tests;

[TestClass]
public class CollisionTest
{
    [TestMethod]
    public void TestMassData()
    {
        Vector2 center = new Vector2(100.0f, -50.0f);
        float hx = 0.5f, hy = 1.5f;
        float angle1 = 0.25f;

        // Data from issue #422. Not used because the data exceeds accuracy limits.
        //const b2Vec2 center(-15000.0f, -15000.0f);
        //const float hx = 0.72f, hy = 0.72f;
        //const float angle1 = 0.0f;

        PolygonShape polygon1 = new PolygonShape(PolygonUtils.CreateRectangle(hx, hy, center, angle1), 1);

        const float absTol = 2.0f * MathConstants.Epsilon;
        const float relTol = 2.0f * MathConstants.Epsilon;

        polygon1.GetMassData(out var massData1);

        Assert.IsTrue(MathUtils.Abs(massData1.Centroid.X - center.X) < absTol + relTol * MathUtils.Abs(center.X));
        Assert.IsTrue(MathUtils.Abs(massData1.Centroid.Y - center.Y) < absTol + relTol * MathUtils.Abs(center.Y));

        Vector2[] vertices = new Vector2[4];
        vertices[0] = new Vector2(center.X - hx, center.Y - hy);
        vertices[1] = new Vector2(center.X + hx, center.Y - hy);
        vertices[2] = new Vector2(center.X - hx, center.Y + hy);
        vertices[3] = new Vector2(center.X + hx, center.Y + hy);

        PolygonShape polygon2 = new PolygonShape(new Vertices(vertices), 1f);
        polygon2.GetMassData(out var massData2);

        Assert.IsTrue(MathUtils.Abs(massData2.Centroid.X - center.X) < absTol + relTol * MathUtils.Abs(center.X));
        Assert.IsTrue(MathUtils.Abs(massData2.Centroid.Y - center.Y) < absTol + relTol * MathUtils.Abs(center.Y));

        float mass = 4.0f * hx * hy;
        float inertia = (mass / 3.0f) * (hx * hx + hy * hy) + mass * MathUtils.Dot(center, center);

        Assert.IsTrue(MathUtils.Abs(massData1.Centroid.X - center.X) < absTol + relTol * MathUtils.Abs(center.X));
        Assert.IsTrue(MathUtils.Abs(massData1.Centroid.Y - center.Y) < absTol + relTol * MathUtils.Abs(center.Y));
        Assert.IsTrue(MathUtils.Abs(massData1.Mass - mass) < 20.0f * (absTol + relTol * mass));
        Assert.IsTrue(MathUtils.Abs(massData1.Inertia - inertia) < 40.0f * (absTol + relTol * inertia));

        Assert.IsTrue(MathUtils.Abs(massData2.Centroid.X - center.X) < absTol + relTol * MathUtils.Abs(center.X));
        Assert.IsTrue(MathUtils.Abs(massData2.Centroid.Y - center.Y) < absTol + relTol * MathUtils.Abs(center.Y));
        Assert.IsTrue(MathUtils.Abs(massData2.Mass - mass) < 20.0f * (absTol + relTol * mass));
        Assert.IsTrue(MathUtils.Abs(massData2.Inertia - inertia) < 40.0f * (absTol + relTol * inertia));
    }
}
