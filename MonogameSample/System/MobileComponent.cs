using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System
{
    class MobileComponent : Component
    {
        public Vector2 Position;
        public Vector2 Velocity;

        // keep track of collision direction
        // -1 -> 1
        public sbyte XCollision { get; set; }
        public sbyte YCollision { get; set; }

        // keep track of whether it's a "steppable" collision (a single tile in the x direction)
        public bool SteppableCollision { get; set; }


        public int Width { get; set; }
        public int Height { get; set; }

        public Vector2 Center => Position + new Vector2(Width, Height) / 2;
        public Vector2 BottomRight => Position + new Vector2(Width, Height);
        public int Left => (int)Position.X;
        public int Right => (int)Position.X + Width;

        public int Top => (int)Position.Y;
        public int Bottom => (int)Position.Y + Height;
        public Rectangle Hitbox => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
    }
}
