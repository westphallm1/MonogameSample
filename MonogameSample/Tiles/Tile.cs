using System;
using System.Collections.Generic;
using System.Text;
using static MonogameSample.Tiles.TileConfiguration;

namespace MonogameSample.Tiles
{
    enum TileType : byte
    {
        DIRT = 0,
        STONE
    }
    enum TileConfiguration : byte
    {
        INACTIVE = 0,
        FULL,
        LEFT,
        RIGHT,
        TOP,
        BOTTOM,
        TOP_LEFT,
        TOP_RIGHT,
        BOTTOM_LEFT,
        BOTTOM_RIGHT
    }

    struct Tile
    {
        public TileType Type { get; set; }
        public TileConfiguration Configuration { get; set; }

        public bool IsActive => Configuration != INACTIVE;


        public Tile(TileType type)
        {
            Type = type;
            Configuration = FULL;
        }
    }
}
