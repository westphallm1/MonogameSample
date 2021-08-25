using Microsoft.Xna.Framework;
using MonogameSample.Tiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System
{
    class MovementSystem
    {
        public static List<MobileComponent> Movers = new List<MobileComponent>();

        public static void Update()
        {
            for(int i = 0; i < Movers.Count; i++)
            {
                MobileComponent mover = Movers[i];
                MoveWithTileCollide(mover);
                mover.Position += mover.Velocity;
            }
        }


        private static void MoveWithTileCollide(MobileComponent mover)
        {
            mover.XCollision = 0;
            mover.YCollision = 0;
            mover.SteppableCollision = false;
            CollideX(mover);
            CollideY(mover);
        }

        private static void CollideX(MobileComponent mover)
        {
            if(mover.Velocity.X == 0) { return;  }
            int nextX = (int)mover.Velocity.X + (mover.Velocity.X > 0 ? mover.Right : mover.Left);
            int tileI = nextX / World.TileSize;
            for(int y = mover.Top + 1; y <= mover.Bottom - 1; y+= World.TileSize)
            {                
                int tileJ = y / World.TileSize;
                bool isLastStep = y + World.TileSize >= mover.Bottom;
                if (World.TileActive(tileI, tileJ))
                {
                    Rectangle collisionBox = World.CollisionBox(tileI, tileJ);
                    mover.XCollision = (sbyte)Math.Sign(mover.Velocity.X);
                    mover.SteppableCollision = isLastStep;
                    if (mover.Velocity.X > 0)
                    {
                        mover.Position.X = collisionBox.Left - mover.Width;
                        mover.Velocity.X = 0;
                    }
                    else
                    {
                        mover.Position.X = collisionBox.Right;
                        mover.Velocity.X = 0;
                    }
                    return;
                } else if (isLastStep)
                {
                    // start moving up the tile before actually bumping into it
                    for(int i = 0; i<3; i++)
                    {
                        nextX += (int)mover.Velocity.X;
                        tileI = nextX / World.TileSize;
                        if(World.TileActive(tileI,tileJ))
                        {
                            mover.SteppableCollision = true;
                            return;
                        }
                    }
                }
                if(y < mover.Bottom - 1 && y + World.TileSize > mover.Bottom - 1)
                {
                    y = mover.Bottom - World.TileSize - 1;
                }
            }
        }

        private static void CollideY(MobileComponent mover)
        {
            if(mover.Velocity.Y == 0) { return;  }
            int nextY = (int)mover.Velocity.Y + (mover.Velocity.Y > 0 ? mover.Bottom : mover.Top);
            int tileJ = nextY / World.TileSize;
            for(int x = mover.Left + 1; x <= mover.Right - 1; x += World.TileSize)
            {
                int tileI = x / World.TileSize;
                if (World.TileActive(tileI, tileJ))
                {
                    Rectangle collisionBox = World.CollisionBox(tileI, tileJ);
                    mover.YCollision = (sbyte)Math.Sign(mover.Velocity.Y);
                    if (mover.Velocity.Y > 0)
                    {
                        mover.Position.Y = collisionBox.Top - mover.Height;
                        mover.Velocity.Y = 0;
                    } else 
                    {
                        mover.Position.Y = collisionBox.Bottom;
                        mover.Velocity.Y = 0;
                    }
                    return;
                }
                if(x < mover.Right - 1 && x + World.TileSize > mover.Right - 1)
                {
                    x = mover.Right - World.TileSize - 1;
                }

            }

        }
    }
}
