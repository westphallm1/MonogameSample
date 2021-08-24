using MonogameSample.System;
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
                new MobileComponent() { Width = 24, Height = 22 },
                new BasicTextureDrawer(TextureCache.playerTexture),
                new PlayerPhysics());
        }
    }
}
