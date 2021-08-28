using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.Utils
{
    class Camera
    {
        public static Camera GameCamera { get; private set; }

        public static void Load()
        {
            GameCamera = new Camera();
        }


        public Vector2 Center;
        
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        public Vector2 ScreenPosition => Center - new Vector2(ScreenWidth, ScreenHeight) /2;
        public Vector2 BottomRight => Center - new Vector2(ScreenWidth, -ScreenHeight) / 2;
        public Vector2 BottomLeft => Center + new Vector2(ScreenWidth, ScreenHeight) / 2;

        public Rectangle ScreenBounds => new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, ScreenWidth, ScreenHeight);

        public void Update(Vector2 center, GameWindow window)
        {
            Center = center;
            ScreenWidth = window.ClientBounds.Width;
            ScreenHeight = window.ClientBounds.Height;
        }
    }
}
