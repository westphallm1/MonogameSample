using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static MonogameSample.Utils.Camera;

namespace MonogameSample.System.Drawing
{
    // Todo maybe a struct
    class TextureLayer
    {
        internal Texture2D Texture;
        internal Rectangle Bounds;
        // from center
        internal Vector2 Offset;
        internal float Rotation;
        internal float Scale;
        internal SpriteEffects SpriteEffects;

        internal bool ShouldDraw;
        // TODO some way to dynamically update

        public TextureLayer(Texture2D texture, Rectangle bounds = default, Vector2 offset = default, float rotation = default, float scale = default)
        {
            Texture = texture;
            Bounds = bounds == default ? texture.Bounds : bounds;
            Offset = offset;
            Rotation = rotation;
            Scale = scale == default ? 1 : scale;
            ShouldDraw = true;
        }
    }

    class FramedTextureLayer : TextureLayer
    {
        private int frameCount;
        private int _frame;
        public int Frame 
        { 
            get => _frame; 
            set 
            {
                _frame = value;
                int frameHeight = Texture.Height / frameCount;
                Bounds = new Rectangle(0, frameHeight * _frame, Texture.Width, frameHeight);
            } 
        }
        public FramedTextureLayer(Texture2D texture, int frameCount, Vector2 offset = default, float rotation = default, float scale = default)
            : base(texture, default, offset, rotation, scale)
        {
            this.frameCount = frameCount;
            Frame = 0;
        }

    }

    class LayeredTextureDrawer : PositionedDrawerComponent
    {
        public TextureLayer[] Layers;

        public LayeredTextureDrawer(params TextureLayer[] layers)
        {
            Layers = layers;
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < Layers.Length; i++)
            {
                TextureLayer drawer = Layers[i];
                if(!drawer.ShouldDraw) { continue; }
                Vector2 position = location.Center + drawer.Offset - GameCamera.ScreenPosition;
                Vector2 origin = new Vector2(drawer.Bounds.Width / 2, drawer.Bounds.Height / 2);
                spriteBatch.Draw(
                    drawer.Texture, position, drawer.Bounds, Color.White, drawer.Rotation, origin, drawer.Scale, drawer.SpriteEffects, 0);
            }
        }
    }
}
