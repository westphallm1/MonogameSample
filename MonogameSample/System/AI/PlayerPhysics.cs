using MonogameSample.Entities;
using MonogameSample.System.AI;
using MonogameSample.System.Drawing;
using MonogameSample.System.Movement;
using MonogameSample.Tiles;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameSample.System.AI
{

    enum PlayerState
    {
        IDLE,
        WALKING,
        JUMPING
    }
    class PlayerPhysics : AIComponent
    {

        // old favorite
        private int Frame;

        private int JumpFrame;

        static readonly int JumpDelay = 15;

        private PlayerState State = PlayerState.IDLE;

        static readonly float MaxVel = 8;
        static readonly float VelStep = 2;
        static readonly float VelDecay = 0.8f;

        private MobileComponent movement;
        private FramedTextureLayer bodyLayer;

        public override void PostAttach()
        {
            base.PostAttach();
            movement = Entity.GetComponent<MobileComponent>();
            LayeredTextureDrawer drawer = Entity.GetComponent<LayeredTextureDrawer>();
            bodyLayer = (FramedTextureLayer)drawer.Layers[0];
        }

        public override void Update()
        {
            Frame++;
            switch(State)
            {
                case PlayerState.IDLE:
                    HandleIdle();
                    break;
                case PlayerState.WALKING:
                    HandleWalking();
                    break;
                case PlayerState.JUMPING:
                    HandleJumping();
                    break;
            }
        }

        private void HandleWalking()
        {
            ProcessLateralInputs();
            ApplyGravity();
            bodyLayer.Frame = (Frame / 5) % 10;
            if (!CheckForJump() && Math.Abs(movement.Velocity.X) < 0.01f)
            {
                State = PlayerState.IDLE;
            }
            // climp up small bumps
            if(movement.XCollision != 0 && movement.Velocity.Y > -2)
            {
                movement.Position.Y -= World.TileSize / 4;
            } 
        }

        private void HandleJumping()
        {
            ProcessLateralInputs();
            ApplyGravity();
            if(movement.YCollision == 1)
            {
                State = Math.Abs(movement.Velocity.X) > 0.01f ? PlayerState.WALKING : PlayerState.IDLE;
            }

            if(movement.Velocity.Y < -1)
            {
                bodyLayer.Frame = 11;
            } else if (movement.Velocity.Y > 1)
            {
                bodyLayer.Frame = 13;
            } else
            {
                bodyLayer.Frame = 12;
            }
        }

        private void HandleIdle()
        {
            ProcessLateralInputs();
            ApplyGravity();
            bodyLayer.Frame = 10;
            if (!CheckForJump() && Math.Abs(movement.Velocity.X) > 0.01f)
            {
                State = PlayerState.WALKING;
            }
        }

        private bool CheckForJump()
        {
            if(InputSystem.Jump && (movement.SteppableCollision || movement.YCollision == 1))
            {
                JumpFrame = Frame;
                movement.Velocity.Y = -8;
                State = PlayerState.JUMPING;
                return true;
            }
            return false;
        }

        private void ProcessLateralInputs()
        {
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
            if(movement.Velocity.X > 0.1f)
            {
                bodyLayer.SpriteEffects = SpriteEffects.None;
            } else if (movement.Velocity.X < -0.1f)
            {
                bodyLayer.SpriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        private void ApplyGravity()
        {
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
