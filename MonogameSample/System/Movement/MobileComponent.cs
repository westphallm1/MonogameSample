using Microsoft.Xna.Framework;
using MonogameSample.System.Movement;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System.Movement
{
    class MobileComponent : PositionedComponent
    {
        public Vector2 Velocity;

        // keep track of collision direction
        // -1 -> 1
        public sbyte XCollision { get; set; }
        public sbyte YCollision { get; set; }

        // keep track of whether it's a "steppable" collision (a single tile in the x direction)
        public bool SteppableCollision { get; set; }
        public override void AddToSystem()
        {
            MovementSystem.Movers.Add(this);
        }
    }
}
