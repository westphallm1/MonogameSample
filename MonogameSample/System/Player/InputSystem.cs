using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System
{
    /// <summary>
    /// Bit of a misnomer, as it applies only to the single player-controlled entity
    /// </summary>
    class InputSystem
    {

        public static bool Left;
        public static bool Right;
        public static bool Up;
        public static bool Down;
        public static bool Jump;
        public static void Update()
        {
            KeyboardState kState = Keyboard.GetState();
            Right = kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D);
            Left = kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A);
            Up = kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W);
            Down = kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S);
            Jump = kState.IsKeyDown(Keys.Space);
        }
    }
}
