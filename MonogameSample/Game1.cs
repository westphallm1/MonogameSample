using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameSample.Entities;
using MonogameSample.System;
using MonogameSample.System.AI;
using MonogameSample.System.Drawing;
using MonogameSample.System.Movement;
using MonogameSample.Tiles;
using MonogameSample.Utils;
using System;

namespace MonogameSample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Entity player;
        private Entity tree;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            _graphics.SynchronizeWithVerticalRetrace = false; //Vsync
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / 60);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Camera.Load();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureCache.Load(Content);
            TileState.Load();
            World.Load(Content);
            GameText.Load(Content);
            player = Player.MakePlayer();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputSystem.Update();
            AISystem.Update();
            MovementSystem.Update();
            Camera.GameCamera.Update(player.GetComponent<MobileComponent>().Center, Window);
            GameText.Update("");
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            Parallax.Draw(_spriteBatch);
            DrawerSystem.Draw(_spriteBatch);
            World.Draw(_spriteBatch);
            GameText.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
