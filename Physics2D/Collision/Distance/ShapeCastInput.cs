﻿using Physics2D.Shared;
using Microsoft.Xna.Framework;

namespace Physics2D.Collision.Distance
{
    /// <summary>Input parameters for b2ShapeCast</summary>
    public struct ShapeCastInput
    {
        public DistanceProxy ProxyA;
        public DistanceProxy ProxyB;
        public Transform TransformA;
        public Transform TransformB;
        public Vector2 TranslationB;
    }
}