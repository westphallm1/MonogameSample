using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System
{
    class DrawerSystem
    {
        public static List<DrawerComponent> Drawers = new List<DrawerComponent>();

        public static void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < Drawers.Count; i++)
            {
                Drawers[i].Draw(spriteBatch);
            }
        }


    }
}
