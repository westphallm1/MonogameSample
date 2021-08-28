using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static MonogameSample.Utils.Camera;

namespace MonogameSample.System.Drawing
{
    // basic implementation
    class BasicTextureDrawer : PositionedDrawerComponent
    {
        private Texture2D texture;
        // not sure about the co-dependence of components here

        public BasicTextureDrawer(Texture2D texture)
        {
            this.texture = texture;
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location.Position - GameCamera.ScreenPosition, Color.White);
        }
    }
}
