using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonogameSample.System;
using MonogameSample.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using static MonogameSample.Utils.Camera;
using static MonogameSample.Tiles.TileType;

namespace MonogameSample.Tiles
{
    class World
    {
        public static Tile[,] tiles;
        public static readonly int TileSize = 16;

        public static int WorldWidth => tiles.GetLength(0);
        public static int WorldHeight => tiles.GetLength(1);
        public static void Load(ContentManager content)
        {
            tiles = new Tile[400, 50];
            for(int i = 0; i < WorldWidth; i++)
            {
                int groundLevel = WorldHeight / 2 + (int)(10 * Math.Sin(MathHelper.TwoPi * i / 50));
                for(int j = 0; j < WorldHeight; j++)
                {
                    if(j > groundLevel)
                    {
                        tiles[i, j] = new Tile(DIRT);
                    }
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // todo screen bounds;
            int iMin = Math.Max(0, (int)(GameCamera.ScreenPosition.X / TileSize) - 1);
            int iMax = Math.Min(WorldWidth, (int)(GameCamera.BottomLeft.X / TileSize) + 1);
            int jMin = Math.Max(0, (int)(GameCamera.ScreenPosition.Y / TileSize) - 1);
            int jMax = Math.Min(WorldHeight, (int)(GameCamera.BottomLeft.Y / TileSize) + 1);
            for(int i = iMin; i < iMax; i++)
            {
                for(int j = jMin; j < jMax; j++)
                {
                    if(tiles[i,j].IsActive)
                    {
                        Vector2 tilePosition = TileSize * new Vector2(i, j);
                        spriteBatch.Draw(
                            TextureCache.tileTexture, 
                            tilePosition - GameCamera.ScreenPosition, 
                            Color.White);
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
