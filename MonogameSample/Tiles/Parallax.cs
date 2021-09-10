using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameSample.System;
using System;
using System.Collections.Generic;
using System.Text;
using static MonogameSample.Utils.Camera;

namespace MonogameSample.Tiles
{
    class Parallax
    {
        readonly static float XPan0 = 4;
        readonly static float YPan0 = 16;
        readonly static float YBaseline0 = 1000;

        readonly static float XPanScale = 2;
        readonly static float YPanScale = 4;
        readonly static float YBaselineScale = -600;
        public static void Draw(SpriteBatch spriteBatch)
        {
            for(int i = TextureCache.parallaxTexture.Length - 1; i >= 0 ; i--)
            {
                DrawParallaxLayer(spriteBatch, i);
            }
        }

        public static void DrawParallaxLayer(SpriteBatch spriteBatch, int layerDepth)
        {
            Vector2 center = GameCamera.Center;
            int screenWidth = GameCamera.ScreenWidth;
            Texture2D texture = TextureCache.parallaxTexture[layerDepth];
            float xPan = XPan0 + layerDepth * XPanScale;
            float yPan = YPan0 + layerDepth * YPanScale;
            float yBaseline = YBaseline0 + layerDepth * YBaselineScale;
            int startX = (int)(-center.X / xPan) % texture.Width;
            int yPos = (int)(GameCamera.ScreenHeight - texture.Height - (center.Y - yBaseline) /yPan);
            // initial stamp, from startpoint to the end of the texture
            Rectangle initialBounds = new Rectangle(texture.Width - startX, 0, startX, texture.Height);
            Vector2 drawPos = new Vector2(0, yPos);
            spriteBatch.Draw(
                texture,
                drawPos, 
                initialBounds,
                Color.White);
            for(int i = startX; i < screenWidth; i+= texture.Width)
            {
                drawPos = new Vector2(i, yPos);
                spriteBatch.Draw(
                    texture,
                    drawPos, 
                    texture.Bounds,
                    Color.White);
            }

        }
    }
}
