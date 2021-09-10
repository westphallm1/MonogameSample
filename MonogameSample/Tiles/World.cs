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
using MonogameSample.Entities;

namespace MonogameSample.Tiles
{
    class World
    {
        public static Tile[,] tiles;
        public static readonly int TileSize = 16;

        public static int WorldWidth => tiles.GetLength(0);
        public static int WorldHeight => tiles.GetLength(1);

        private static FastNoiseLite terrainNoise;
        private static FastNoiseLite hillNoise;

        private static List<Action> TerrainGenerators;

        public static Random random;

        private static void SetupNoises(int seed)
        {
            terrainNoise = new FastNoiseLite();
            terrainNoise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            terrainNoise.SetSeed(seed);
            terrainNoise.SetFrequency(0.03f);

            hillNoise = new FastNoiseLite();
            hillNoise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            hillNoise.SetSeed(seed);
            hillNoise.SetFrequency(0.01f);

            random = new Random(seed);
        }
        public static void Load(ContentManager content)
        {
            tiles = new Tile[800, 100];
            Lighting.lightLevels = new byte[WorldWidth, WorldHeight];
            SetupNoises((int)DateTimeOffset.Now.ToUnixTimeMilliseconds());

            // todo dynamically inject a list of generators
            TerrainGenerators = new List<Action>
            {
                CreateGround,
                CreateTunnels,
                PlaceTrees
            };

            TerrainGenerators.ForEach(g => g.Invoke());

            for (int i = 0; i < WorldWidth; i++)
            {
                for(int j = 0; j < WorldHeight; j++)
                {
                    if(tiles[i,j].IsActive)
                    {
                        TileState.UpdateTileState(i, j);
                        Lighting.AddLight(i, j);
                    }
                }
            }
        }

        public static float GetGroundLevel(int i)
        {
            return 50 + 10 * terrainNoise.GetNoise(i, 0) +  50 * Math.Max(0, hillNoise.GetNoise(i,0));
        }

        public static void CreateGround()
        {
            for(int i = 0; i < WorldWidth; i++)
            {
                float groundLevel = GetGroundLevel(i);

                float grassHeight = 4 + 2 * terrainNoise.GetNoise(i, i);

                float stoneHeight = 14 + 4 * terrainNoise.GetNoise(i, i);
                for(int j = 0; j < WorldHeight; j++)
                {
                    if(j > groundLevel + stoneHeight)
                    {
                        tiles[i, j] = new Tile(STONE);
                    } else if(j > groundLevel + grassHeight)
                    {
                        tiles[i, j] = new Tile(DIRT);
                    } else if (j > groundLevel)
                    {
                        tiles[i, j] = new Tile(GRASS);
                    }
                }
            }
        }

        public static void CreateTunnels()
        {
            for(int i = 50; i < WorldWidth - 50; i+= 150)
            {
                CreateTunnel(i, 10);
            }
        }

        public static void CreateTunnel(int startI, int startJ, bool followGround = true)
        {
            int tunnelLength = (int)(30 + 12 * terrainNoise.GetNoise(startI * 2, startI * 2));
            int startOffset = (int)(10 * terrainNoise.GetNoise(0, startI));
            for(int i = startI + startOffset; i < startI + startOffset + tunnelLength; i++)
            {
                int height = (int)(4 + 2 * terrainNoise.GetNoise(i, i + 100));
                int startHeight = (int)GetGroundLevel(i) - startJ - height/2;
                for(int j = startHeight; j < startHeight + height; j++)
                {
                    tiles[i, j].Configuration = TileConfiguration.FULL;
                    tiles[i, j].Type = GRASS;
                }

            }
        }

        public static void PlaceTrees()
        {
            for (int i = 10; i < WorldWidth - 10; i++)
            {
                // probe downwards
                int frequency = (int)(10 - 4 * (1 + terrainNoise.GetNoise(i, 30)) / 2);
                if(i % frequency != 0) { continue; }
                int j;
                for(j = 0; j < WorldHeight; j++)
                {
                    if(tiles[i, j].IsActive) { break; }
                }
                if(tiles[i, j].Type != GRASS) { continue; }
                if(CheckFlatGround(i - 1, j, 3))
                {
                    Scenery.PlaceTree(i, j);
                }
            }
        }

        public static bool CheckFlatGround(int startI, int j, int width)
        {
            for(int i = startI; i < startI + width; i++)
            {
                if(!tiles[i, j].IsActive || tiles[i, j-1].IsActive)
                {
                    return false;
                }
            }
            return true;
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
                        TileState.DrawTile(spriteBatch, tiles[i,j], i, j);
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
