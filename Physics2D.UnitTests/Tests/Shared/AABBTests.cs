using Physics2D.Shared;
using Microsoft.Xna.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics2D.UnitTests.Shared;

[TestClass]
public class AABBTests
{
    [TestMethod]
    public void TestOverlap()
    {
        {
            AABB bb1 = new AABB(new Vector2(-2, -3), new Vector2(-1, 0));
            Assert.IsTrue(AABB.TestOverlap(ref bb1, ref bb1));
        }
        {
            Vector2 vec = new Vector2(-2, -3);
            AABB bb1 = new AABB(vec, vec);
            Assert.IsTrue(AABB.TestOverlap(ref bb1, ref bb1));
        }
        {
            AABB bb1 = new AABB(new Vector2(-2, -3), new Vector2(-1, 0));
            AABB bb2 = new AABB(new Vector2(-1, -1), new Vector2(1, 2));
            Assert.IsTrue(AABB.TestOverlap(ref bb1, ref bb2));
        }
        {
            AABB bb1 = new AABB(new Vector2(-99, -3), new Vector2(-1, 0));
            AABB bb2 = new AABB(new Vector2(76, -1), new Vector2(-2, 2));
            Assert.IsTrue(AABB.TestOverlap(ref bb1, ref bb2));
        }
        {
            AABB bb1 = new AABB(new Vector2(-20, -3), new Vector2(-18, 0));
            AABB bb2 = new AABB(new Vector2(-1, -1), new Vector2(1, 2));
            Assert.IsFalse(AABB.TestOverlap(ref bb1, ref bb2));
        }
        {
            AABB bb1 = new AABB(new Vector2(-2, -3), new Vector2(-1, 0));
            AABB bb2 = new AABB(new Vector2(-1, +1), new Vector2(1, 2));
            Assert.IsFalse(AABB.TestOverlap(ref bb1, ref bb2));
        }
        {
            AABB bb1 = new AABB(new Vector2(-2, +3), new Vector2(-1, 0));
            AABB bb2 = new AABB(new Vector2(-1, -1), new Vector2(0, -2));
            Assert.IsFalse(AABB.TestOverlap(ref bb1, ref bb2));
        }
    }
}
