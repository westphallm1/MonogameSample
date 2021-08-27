using System;
using System.Collections.Generic;
using System.Text;
using static MonogameSample.Tiles.World;

namespace MonogameSample.Tiles
{
    struct LightLevelQueueEntry
    {
        internal int I;
        internal int J;
        internal sbyte Level;

        public LightLevelQueueEntry(int i, int j, sbyte level)
        {
            I = i;
            J = j;
            Level = level;
        }
    }
    class Lighting
    {
        public static byte[,] lightLevels;
        public static readonly int MaxLightLevel = 20;

        public static void AddLight(int i, int j)
        {
            if(!tiles[i, j].IsActive || !GetLightForTile(i, j, out sbyte level)) { return; }
            // 'flood' the adjacent tiles with light
            Queue<LightLevelQueueEntry> lightQueue = new Queue<LightLevelQueueEntry>();
            lightQueue.Enqueue(new LightLevelQueueEntry(i, j, level));
            while(lightQueue.Count > 0)
            {
                LightLevelQueueEntry entry = lightQueue.Dequeue();
                lightLevels[entry.I, entry.J] = (byte)entry.Level;
                int iMin = Math.Max(0, entry.I - 1);
                int iMax = Math.Min(WorldWidth -1, entry.I+1);
                int jMin = Math.Max(0, entry.J - 1);
                int jMax = Math.Min(WorldHeight - 1, entry.J+1);
                for(int x = iMin; x <= iMax; x++)
                {
                    for(int y = jMin; y <= jMax; y++)
                    {
                        sbyte nextLevel = (sbyte)(entry.Level - (x == entry.I || y == entry.J ? 2 : 4));
                        if(tiles[x,y].Type == TileType.AIR || lightLevels[x,y] >= nextLevel) { continue; }
                        lightQueue.Enqueue(new LightLevelQueueEntry(x, y, nextLevel));
                    }
                }

            }
        }

        public static bool GetLightForTile(int i, int j, out sbyte level)
        {
            // todo figure out how to remove light
            // currently, light just comes from the air
            int iMin = Math.Max(0, i - 1);
            int iMax = Math.Min(WorldWidth -1, i+1);
            int jMin = Math.Max(0, j - 1);
            int jMax = Math.Min(WorldHeight -1, j+1);
            for(int x = iMin; x <= iMax; x++)
            {
                for(int y = jMin; y <= jMax; y++)
                {
                    if(tiles[x,y].Type == TileType.AIR)
                    {
                        level = (sbyte)MaxLightLevel;
                        return true;
                    }
                }
            }
            level = -1;
            return false;
        }
    }
}
