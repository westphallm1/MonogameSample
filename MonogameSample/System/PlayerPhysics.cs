using MonogameSample.Entities;
using MonogameSample.Tiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System
{
    class PlayerPhysics : Component
    {

        // old favorite
        private int Frame;

        private int JumpFrame;

        static readonly int JumpDelay = 15;

        static readonly float MaxVel = 8;
        static readonly float VelStep = 2;
        static readonly float VelDecay = 0.8f;

        public PlayerPhysics()
        {
        }

        public void Update()
        {
            MobileComponent movement = Entity.GetComponent<MobileComponent>();
            Frame++;
            if(InputSystem.Left && movement.Velocity.X <= 0.5f)
            {
                movement.Velocity.X -= VelStep;
            } else if(InputSystem.Right && movement.Velocity.X >= -0.5f)
            {
                movement.Velocity.X += VelStep;
            } else
            {
                movement.Velocity.X *= VelDecay;
            }
            if(Math.Abs(movement.Velocity.X) > MaxVel)
            {
                movement.Velocity.X = MaxVel * Math.Sign(movement.Velocity.X);
            }

            // (do you believe in) gravity
            if(InputSystem.Jump && (movement.SteppableCollision || movement.YCollision == 1))
            {
                JumpFrame = Frame;
                movement.Velocity.Y = -8;
            }
            // climp up small bumps
            if(movement.SteppableCollision && movement.Velocity.Y > -2)
            {
                movement.Position.Y -= World.TileSize / 4;
                movement.Velocity.Y = 0;
            } 
            bool Ascending = Frame - JumpFrame < JumpDelay && InputSystem.Jump;
            if(movement.Velocity.Y < 16 && !Ascending)
            {
                movement.Velocity.Y += 0.5f;
            }
            if(movement.Velocity.Y < 20 && InputSystem.Down)
            {
                movement.Velocity.Y += 1f;
            }
        }
    }
}
