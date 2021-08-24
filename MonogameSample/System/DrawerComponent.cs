using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static MonogameSample.Utils.Camera;

namespace MonogameSample.System
{
    /// <summary>
    /// Abstract so that each class can override
    /// </summary>
    abstract class DrawerComponent : Component
    {
        internal abstract void Draw(SpriteBatch spriteBatch);
    }

    // basic implementation
    class BasicTextureDrawer : DrawerComponent
    {
        private Texture2D texture;
        // not sure about the co-dependence of components here

        public BasicTextureDrawer(Texture2D texture)
        {
            this.texture = texture;
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            MobileComponent mover = Entity.GetComponent<MobileComponent>();
            spriteBatch.Draw(texture, mover.Position - GameCamera.ScreenPosition, Color.White);
        }
    }
}
