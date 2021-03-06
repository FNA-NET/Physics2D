using Physics2D.Collision.ContactSystem;
using Physics2D.Dynamics;
using Physics2D.Factories;
using Physics2D.Samples.Testbed.Framework;
using Microsoft.Xna.Framework;

namespace Physics2D.Samples.Testbed.Tests.Velcro
{
    public class LockTest : Test
    {
        private readonly Body _rectangle;

        private LockTest()
        {
            BodyFactory.CreateEdge(World, new Vector2(-20, 0), new Vector2(20, 0));

            _rectangle = BodyFactory.CreateRectangle(World, 2, 2, 1);
            _rectangle.BodyType = BodyType.Dynamic;
            _rectangle.Position = new Vector2(0, 10);
            _rectangle.OnCollision += OnCollision;

            //Properties and methods that were checking for lock before
            //Body.Enabled
            //Body.LocalCenter
            //Body.Mass
            //Body.Inertia
            //Fixture.DestroyFixture()
            //Body.SetTransformIgnoreContacts()
            //Fixture()
        }

        private void OnCollision(Fixture fixturea, Fixture fixtureb, Contact manifold)
        {
            //_rectangle.CreateFixture(_rectangle.Shape); //Calls the constructor in Fixture
            //_rectangle.DestroyFixture(_rectangle);
            //_rectangle.Inertia = 40;
            //_rectangle.LocalCenter = new Vector2(-1, -1);
            //_rectangle.Mass = 10;
            _rectangle.Enabled = false;
        }

        internal static Test Create()
        {
            return new LockTest();
        }
    }
}