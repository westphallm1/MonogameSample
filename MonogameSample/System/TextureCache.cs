using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace MonogameSample.System
{
    /// <summary>
    /// Big ol' static class to cache textures
    /// </summary>
    class TextureCache
    {
        public static Texture2D playerTexture;
        public static Texture2D tileTexture;



        public static void Load(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("blob-man");
            tileTexture = content.Load<Texture2D>("brick");
        }
    }
}
