/*
* Velcro Physics:
* Copyright (c) 2017 Ian Qvist
* 
* Original source Box2D:
* Copyright (c) 2006-2011 Erin Catto http://www.box2d.org 
* 
* This software is provided 'as-is', without any express or implied 
* warranty.  In no event will the authors be held liable for any damages 
* arising from the use of this software. 
* Permission is granted to anyone to use this software for any purpose, 
* including commercial applications, and to alter it and redistribute it 
* freely, subject to the following restrictions: 
* 1. The origin of this software must not be misrepresented; you must not 
* claim that you wrote the original software. If you use this software 
* in a product, an acknowledgment in the product documentation would be 
* appreciated but is not required. 
* 2. Altered source versions must be plainly marked as such, and must not be 
* misrepresented as being the original software. 
* 3. This notice may not be removed or altered from any source distribution. 
*/

using System;
using Physics2D.Collision.ContactSystem;
using Physics2D.Collision.Narrowphase;
using Physics2D.Collision.Shapes;
using Physics2D.Dynamics;
using Physics2D.Dynamics.Solver;
using Physics2D.Factories;
using Physics2D.Samples.Testbed.Framework;
using Physics2D.Shared;
using Physics2D.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics2D.Samples.Testbed.Tests.Velcro
{
    public class RockBreakTest : Test
    {
        private readonly Body _body1;
        private readonly Fixture _piece1;
        private readonly PolygonShape _shape2;
        private float _angularVelocity;
        private bool _break;
        private bool _broke;
        private Fixture _piece2;
        private Vector2 _velocity;

        private RockBreakTest()
        {
            // Ground body
            BodyFactory.CreateEdge(World, new Vector2(-40.0f, 0.0f), new Vector2(40.0f, 0.0f));

            // Breakable dynamic body
            _body1 = BodyFactory.CreateBody(World);
            _body1.BodyType = BodyType.Dynamic;
            _body1.Position = new Vector2(0.0f, 40.0f);
            _body1.Rotation = 0.25f * MathConstants.Pi;

            Vertices box = PolygonUtils.CreateRectangle(0.5f, 0.5f, new Vector2(-0.5f, 0.0f), 0.0f);

            PolygonShape shape1 = new PolygonShape(box, 1);
            _piece1 = _body1.AddFixture(shape1);

            box = PolygonUtils.CreateRectangle(0.5f, 0.5f, new Vector2(0.5f, 0.0f), 0.0f);
            _shape2 = new PolygonShape(box, 1);

            _piece2 = _body1.AddFixture(_shape2);

            _break = false;
            _broke = false;
        }

        public override void Initialize()
        {
            //load texture that will represent the physics body
            Texture2D polygonTexture = GameInstance.Content.Load<Texture2D>("Rock");

            //Create an array to hold the data from the texture
            uint[] data = new uint[polygonTexture.Width * polygonTexture.Height];

            //Transfer the texture data to the array
            polygonTexture.GetData(data);

            Vertices verts = PolygonUtils.CreatePolygon(data, polygonTexture.Width);
            Vector2 scale = new Vector2(0.07f, 0.07f);
            verts.Scale(ref scale);

            BodyFactory.CreateBreakableBody(World, verts, 50, new Vector2(-10, 25));

            base.Initialize();
        }

        protected override void PostSolve(Contact contact, ContactVelocityConstraint contactConstraint)
        {
            if (_broke)

                // The body already broke.
                return;

            // Should the body break?
            float maxImpulse = 0.0f;
            Manifold manifold = contact.Manifold;

            for (int i = 0; i < manifold.PointCount; ++i)
                maxImpulse = Math.Max(maxImpulse, contactConstraint.Points[i].NormalImpulse);

            if (maxImpulse > 40.0f)

                // Flag the body for breaking.
                _break = true;
        }

        private void Break()
        {
            // Create two bodies from one.
            Body body1 = _piece1.Body;
            Vector2 center = body1.WorldCenter;

            body1.RemoveFixture(_piece2);
            _piece2 = null;

            Body body2 = BodyFactory.CreateBody(World);
            body2.BodyType = BodyType.Dynamic;
            body2.Position = body1.Position;
            body2.Rotation = body1.Rotation;

            _piece2 = body2.AddFixture(_shape2);

            // Compute consistent velocities for new bodies based on
            // cached velocity.
            Vector2 center1 = body1.WorldCenter;
            Vector2 center2 = body2.WorldCenter;

            Vector2 velocity1 = _velocity + MathUtils.Cross(_angularVelocity, center1 - center);
            Vector2 velocity2 = _velocity + MathUtils.Cross(_angularVelocity, center2 - center);

            body1.AngularVelocity = _angularVelocity;
            body1.LinearVelocity = velocity1;

            body2.AngularVelocity = _angularVelocity;
            body2.LinearVelocity = velocity2;
        }

        public override void Update(GameSettings settings, GameTime gameTime)
        {
            if (_break)
            {
                Break();
                _broke = true;
                _break = false;
            }

            // Cache velocities to improve movement on breakage.
            if (!_broke)
            {
                _velocity = _body1.LinearVelocity;
                _angularVelocity = _body1.AngularVelocity;
            }

            base.Update(settings, gameTime);
        }

        internal static Test Create()
        {
            return new RockBreakTest();
        }
    }
}