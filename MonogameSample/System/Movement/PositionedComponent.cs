using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System.Movement
{
    class PositionedComponent : Component
    {
        public Vector2 Position;
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
