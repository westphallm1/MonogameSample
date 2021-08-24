using MonogameSample.Entities;
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
            if(InputSystem.Jump && movement.YCollision == 1)
            {
                JumpFrame = Frame;
                movement.Velocity.Y = -8;
            }
            if(movement.Velocity.Y < 16 && Frame - JumpFrame > JumpDelay)
            {
                movement.Velocity.Y += 0.5f;
            }
        }
    }
}
