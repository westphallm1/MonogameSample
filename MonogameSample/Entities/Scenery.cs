using MonogameSample.System;
using MonogameSample.System.Drawing;
using MonogameSample.System.Movement;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MonogameSample.Tiles;

namespace MonogameSample.Entities
{
    class Scenery
    {
        public static List<Entity> scenery = new List<Entity>();
        public static Entity PlaceTree(int i, int j)
        {
            int treeSize = 96;
            Vector2 basePosition = new Vector2(i, j) * World.TileSize;
            Vector2 position = basePosition - new Vector2(treeSize / 2 - World.TileSize/2, treeSize - 2);

            int trunkIdx = World.random.Next(2);
            int folliageIdx = World.random.Next(2);
            Vector2 folliageOffset = Vector2.UnitY * World.random.Next(-4, 6);
            Entity tree = new Entity(
                EntityTag.SCENERY,
                new PositionedComponent() { Width = treeSize, Height = treeSize, Position = position },
                new LayeredTextureDrawer(
                    new TextureLayer(TextureCache.trunkTexture, new Rectangle(treeSize * trunkIdx, 0, treeSize, treeSize)),
                    new TextureLayer(
                        TextureCache.folliageTexture, 
                        new Rectangle(treeSize * folliageIdx, 0, treeSize, treeSize), 
                        folliageOffset, 
                        0)
                ));
            scenery.Add(tree);
            return tree;
        }
    }
}
