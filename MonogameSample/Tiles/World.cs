using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonogameSample.System;
using System;
using System.Collections.Generic;
using System.Text;
using static MonogameSample.Tiles.TileType;

namespace MonogameSample.Tiles
{
    class World
    {
        public static Tile[,] tiles;
        public static readonly int TileSize = 16;
        public static void Load(ContentManager content)
        {
            tiles = new Tile[50, 40];
            for(int i = 0; i < 30; i++)
            {
                for(int j = 10; j < 20; j++)
                {
                    tiles[i, j] = new Tile(DIRT);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // todo screen bounds;
            for(int i = 0; i < tiles.GetLength(0); i++)
            {
                for(int j = 0; j < tiles.GetLength(1); j++)
                {
                    if(tiles[i,j].IsActive)
                    {
                        Vector2 tilePosition = TileSize * new Vector2(i, j);
                        spriteBatch.Draw(TextureCache.tileTexture, tilePosition, Color.White);
                    }
                }
            }
        }

        public static bool TileActive(int i, int j) => i < 0 || j < 0 || i >= tiles.GetLength(0) || j >= tiles.GetLength(1) || tiles[i, j].IsActive;

        public static Rectangle CollisionBox(int i, int j)
        {
            return new Rectangle(TileSize * i, TileSize * j, TileSize, TileSize);
        }
    }
}
