using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Components {
    class PositionComponent : ECS.Component {
        public Vector2 Vector2;

        public float X { get => Vector2.X; set => Vector2.X = value; }
        public float Y { get => Vector2.Y; set => Vector2.Y = value; }
    }
}
