using Physics2D.Dynamics;
using Physics2D.Extensions.Controllers.Buoyancy;
using Physics2D.Factories;
using Physics2D.Samples.Testbed.Framework;
using Physics2D.Shared;
using Microsoft.Xna.Framework;

namespace Physics2D.Samples.Testbed.Tests.Velcro
{
    public class BuoyancyTest : Test
    {
        private BuoyancyTest()
        {
            World.Gravity = new Vector2(0, -9.82f);

            BodyFactory.CreateEdge(World, new Vector2(-40, 0), new Vector2(40, 0));

            float offset = 5;
            for (int i = 0; i < 3; i++)
            {
                Body rectangle = BodyFactory.CreateRectangle(World, 2, 2, 1, new Vector2(-30 + offset, 20));
                rectangle.Rotation = Rand.RandomFloat(0, 3.14f);
                rectangle.BodyType = BodyType.Dynamic;
                offset += 7;
            }

            for (int i = 0; i < 3; i++)
            {
                Body rectangle = BodyFactory.CreateCircle(World, 1, 1, new Vector2(-30 + offset, 20));
                rectangle.Rotation = Rand.RandomFloat(0, 3.14f);
                rectangle.BodyType = BodyType.Dynamic;
                offset += 7;
            }

            AABB container = new AABB(new Vector2(0, 10), 60, 10);
            BuoyancyController buoyancy = new BuoyancyController(container, 2, 2, 1, World.Gravity);
            World.AddController(buoyancy);
        }

        internal static Test Create()
        {
            return new BuoyancyTest();
        }
    }
}