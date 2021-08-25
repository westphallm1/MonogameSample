using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameSample.System;
using static MonogameSample.Utils.Camera;
using System;
using System.Collections.Generic;
using System.Text;
using static MonogameSample.Tiles.TileConfiguration;
using static MonogameSample.Tiles.World;

namespace MonogameSample.Tiles
{
    class Framing
    {
        private static Vector2[] offsets;
        private static Rectangle backRectangle = new Rectangle(TileSize, TileSize, TileSize, TileSize);

        public static void Load()
        {
            offsets = new Vector2[(byte)BOTTOM_RIGHT + 1];
            offsets[(byte)FULL] = new Vector2(1, 1);
            offsets[(byte)LEFT] = new Vector2(0, 1);
            offsets[(byte)RIGHT] = new Vector2(4, 1);
            offsets[(byte)TOP] = new Vector2(1, 0);
            offsets[(byte)BOTTOM] = new Vector2(1, 4);
            offsets[(byte)TOP_LEFT] = new Vector2(0, 0);
            offsets[(byte)TOP_RIGHT] = new Vector2(4, 0);
            offsets[(byte)BOTTOM_LEFT] = new Vector2(0, 4);
            offsets[(byte)BOTTOM_RIGHT] = new Vector2(4, 4);
        }

        public static void FrameTile(int i, int j)
        {
            if(!tiles[i,j].IsActive) { return; }
            TileType activeType = tiles[i, j].Type;

            bool top = j == 0 || tiles[i, j - 1].IsType(activeType);
            bool bottom = j == WorldHeight - 1 || tiles[i, j + 1].IsType(activeType);
            bool left = i == 0 || tiles[i - 1, j].IsType(activeType);
            bool right = i == WorldWidth - 1 || tiles[i + 1, j].IsType(activeType);
            if(top && bottom && left && !right)
            {
                tiles[i, j].Configuration = RIGHT;
            } else if (top && bottom && !left && right)
            {
                tiles[i, j].Configuration = LEFT;
            } else if (top && !bottom && left && right)
            {
                tiles[i, j].Configuration = BOTTOM;
            } else if (!top && bottom && left && right)
            {
                tiles[i, j].Configuration = TOP;
            } else if (top && !bottom && left && !right)
            {
                tiles[i, j].Configuration = BOTTOM_RIGHT;
            } else if (top && !bottom && !left && right)
            {
                tiles[i, j].Configuration = BOTTOM_LEFT;
            } else if (!top && bottom && left && !right)
            {
                tiles[i, j].Configuration = TOP_RIGHT;
            } else if (!top && bottom && !left && right)
            {
                tiles[i, j].Configuration = TOP_LEFT;
            } else if (top && bottom && left && right)
            {
                tiles[i, j].Configuration = FULL;
            }  else 
            {
                // prune any single tiles because we don't have appropriate frames for them
                tiles[i, j].Configuration = INACTIVE;
            }

            //blend down and to the right
            if(!bottom)
            {
                if (!left && i > 0 && tiles[i - 1, j].IsActive)
                {
                    tiles[i, j].AdjacentType = tiles[i - 1, j].Type;
                    // need to re-frame predecessor (bad!)
                    FrameTile(i - 1, j);
                } 
                else if (j < WorldHeight - 1 && tiles[i, j + 1].IsActive)
                {
                    tiles[i, j].AdjacentType = tiles[i, j + 1].Type;
                } 
                else if (!right && i < WorldWidth - 1 && tiles[i + 1, j].IsActive)
                {
                    tiles[i, j].AdjacentType = tiles[i + 1, j].Type;
                }
            }
            tiles[i, j].Bounds = GetBounds(ref tiles[i, j], i, j);
        }

        public static Rectangle GetBounds(ref Tile tile, int i, int j)
        {
            if(!tile.IsActive) { return default; }
            Vector2 offset = offsets[(byte)tile.Configuration] * TileSize;
            Rectangle rect = new Rectangle((int)offset.X, (int)offset.Y, TileSize, TileSize);
            switch(tile.Configuration)
            {
                case LEFT:
                case RIGHT:
                    rect.Offset(0, TileSize * (j % 3));
                    break;
                case TOP:
                case BOTTOM:
                    rect.Offset(TileSize * (i % 3), 0);
                    break;
                case FULL:
                    rect.Offset(TileSize * (i % 3), TileSize * (j % 3));
                    break;

            }
            return rect;
        }

        public static void DrawTile(SpriteBatch spriteBatch, Tile tile, int i, int j)
        {
            Vector2 tilePosition = TileSize * new Vector2(i, j);
            if(tile.AdjacentType != TileType.AIR)
            {
                spriteBatch.Draw(
                    TextureCache.tileTexture[(byte)tile.AdjacentType], 
                    tilePosition - GameCamera.ScreenPosition, 
                    backRectangle,
                    Color.White);
            }
            spriteBatch.Draw(
                TextureCache.tileTexture[(byte)tile.Type], 
                tilePosition - GameCamera.ScreenPosition, 
                tile.Bounds,
                Color.White);
            if(tile.AdjacentType == TileType.AIR && tile.Configuration != FULL)
            {
                // todo draw outline
            spriteBatch.Draw(
                TextureCache.outlineRoughTexture,
                tilePosition - GameCamera.ScreenPosition, 
                tile.Bounds,
                Color.White);
            }
        }
    }
}
