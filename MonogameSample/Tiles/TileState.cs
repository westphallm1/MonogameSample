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
    public struct TileNeighbors
    {
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Top { get; set; }
        public bool Bottom { get; set; }

        internal TileNeighbors(int i, int j, TileType activeType)
        {
            Top = j == 0 || tiles[i, j - 1].IsType(activeType);
            Bottom = j == WorldHeight - 1 || tiles[i, j + 1].IsType(activeType);
            Left = i == 0 || tiles[i - 1, j].IsType(activeType);
            Right = i == WorldWidth - 1 || tiles[i + 1, j].IsType(activeType);
        }

        internal TileConfiguration GetConfiguration() => this switch
        {
            { Top: true, Bottom: true, Left: true, Right: false} => RIGHT,
            { Top: true, Bottom: true, Left: false, Right: true} => LEFT,
            { Top: true, Bottom: false, Left: true, Right: true} => BOTTOM,
            { Top: false, Bottom: true, Left: true, Right: true} => TOP,
            { Top: true, Bottom: false, Left: true, Right: false} => BOTTOM_RIGHT,
            { Top: true, Bottom: false, Left: false, Right: true} => BOTTOM_LEFT,
            { Top: false, Bottom: true, Left: true, Right: false} => TOP_RIGHT,
            { Top: false, Bottom: true, Left: false, Right: true} => TOP_LEFT,
            { Top: true, Bottom: true, Left: true, Right: true} => FULL,
            _ => FULL
        };
    }
    class TileState
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

        public static void UpdateTileState(int i, int j)
        {
            if(!tiles[i,j].IsActive) { return; }
            TileType activeType = tiles[i, j].Type;
            TileNeighbors neighbors = new TileNeighbors(i, j, activeType);
            tiles[i, j].Configuration = neighbors.GetConfiguration();
            tiles[i, j].Bounds = GetBounds(ref tiles[i, j], i, j);
            BlendTile(neighbors, i, j);
            Lighting.AddLight(i, j);
        }

        public static void BlendTile(int i, int j)
        {
            TileNeighbors neighbors = new TileNeighbors(i, j, tiles[i, j].Type);
            BlendTile(neighbors, i, j);
        }

        /// <summary>
        /// Blend tile down and to the right
        /// </summary>
        /// <param name="neighbors"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public static void BlendTile(TileNeighbors neighbors, int i, int j)
        {
            // left
            if (!neighbors.Left && i > 0 && tiles[i - 1, j].IsActive)
            {
                tiles[i, j].AdjacentType = tiles[i - 1, j].Type;
                // need to re-frame predecessor (bad!)
                // todo unroll tail recursion
                UpdateTileState(i - 1, j);
            } 
            // bottom
            else if (!neighbors.Bottom && j < WorldHeight - 1 && tiles[i, j + 1].IsActive)
            {
                tiles[i, j].AdjacentType = tiles[i, j + 1].Type;
            } 
            // right
            else if (!neighbors.Right && i < WorldWidth - 1 && tiles[i + 1, j].IsActive)
            {
                tiles[i, j].AdjacentType = tiles[i + 1, j].Type;
            }
            // don't blend upwards, may be a problem later
        }

        /// <summary>
        /// Get the specific frame for the tile based on its configuration and frame
        /// TODO: Replace with byte index into a cached array of Rectangles
        /// </summary>
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
            float lightLevel = Lighting.lightLevels[i, j] / (float) Lighting.MaxLightLevel;
            Color drawColor = Color.White * lightLevel;
            drawColor.A = 255;
            Vector2 tilePosition = TileSize * new Vector2(i, j);
            if(tile.AdjacentType != TileType.AIR)
            {
                spriteBatch.Draw(
                    TextureCache.tileTexture[(byte)tile.AdjacentType], 
                    tilePosition - GameCamera.ScreenPosition, 
                    backRectangle,
                    drawColor);
            }
            spriteBatch.Draw(
                TextureCache.tileTexture[(byte)tile.Type], 
                tilePosition - GameCamera.ScreenPosition, 
                tile.Bounds,
                drawColor);
            if(tile.AdjacentType == TileType.AIR && tile.Configuration != FULL)
            {
                // todo draw outline
            spriteBatch.Draw(
                TextureCache.outlineRoughTexture,
                tilePosition - GameCamera.ScreenPosition, 
                tile.Bounds,
                drawColor);
            }
        }
    }
}
