using MonogameSample.System;
using MonogameSample.System.AI;
using MonogameSample.System.Drawing;
using MonogameSample.System.Movement;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.Entities
{
    class Player
    {
        public static Entity MakePlayer()
        {
            return new Entity(
                EntityTag.PLAYER,
                new MobileComponent() { Width = 30, Height = 40 },
                new LayeredTextureDrawer(new FramedTextureLayer(TextureCache.playerTexture, 14)),
                new PlayerPhysics());
        }
    }
}
