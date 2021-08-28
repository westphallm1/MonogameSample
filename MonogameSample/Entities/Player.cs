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
                new MobileComponent() { Width = 24, Height = 22 },
                new BasicTextureDrawer(TextureCache.playerTexture),
                new PlayerPhysics());
        }
    }
}
