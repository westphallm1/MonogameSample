using Microsoft.Xna.Framework.Graphics;
using MonogameSample.System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System.Drawing
{
    class DrawerSystem
    {
        public static List<DrawerComponent> Drawers = new List<DrawerComponent>();

        public static void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < Drawers.Count; i++)
            {
                if(Drawers[i].ShouldDraw())
                {
                    Drawers[i].Draw(spriteBatch);
                }
            }
        }
    }
}
