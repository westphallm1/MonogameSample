using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameSample.System.Movement;
using System;
using System.Collections.Generic;
using System.Text;
using static MonogameSample.Utils.Camera;

namespace MonogameSample.System.Drawing
{
    /// <summary>
    /// Abstract so that each class can override
    /// </summary>
    abstract class DrawerComponent : Component
    {
        internal abstract bool ShouldDraw();
        internal abstract void Draw(SpriteBatch spriteBatch);

        public override void AddToSystem()
        {
            DrawerSystem.Drawers.Add(this);
        }
    }

    /// <summary>
    /// Drawer component for an item that exists in the world
    /// </summary>
    abstract class PositionedDrawerComponent : DrawerComponent
    {
        internal PositionedComponent location;
        public override void PostAttach()
        {
            location = Entity.GetComponent<PositionedComponent>();
        }

        internal override bool ShouldDraw()
        {
            return GameCamera.ScreenBounds.Intersects(location.Hitbox);
        }
    }

}
