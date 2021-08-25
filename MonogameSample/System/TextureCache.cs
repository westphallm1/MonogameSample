using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using MonogameSample.Tiles;

namespace MonogameSample.System
{
    /// <summary>
    /// Big ol' static class to cache textures
    /// </summary>
    class TextureCache
    {
        public static Texture2D playerTexture;
        public static Texture2D[] tileTexture;
        public static Texture2D outlineRoughTexture;
        public static Texture2D outlineSmoothTexture;




        public static void Load(ContentManager content)
        {
            tileTexture = new Texture2D[20];
            playerTexture = content.Load<Texture2D>("blob-man");
            outlineRoughTexture = content.Load<Texture2D>("outline-rough");
            tileTexture[(int)TileType.GRASS] = content.Load<Texture2D>("grass");
            tileTexture[(int)TileType.DIRT] = content.Load<Texture2D>("dirt");
            tileTexture[(int)TileType.STONE] = content.Load<Texture2D>("stonet");
        }
    }
}
