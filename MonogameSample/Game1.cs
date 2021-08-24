using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameSample.Entities;
using MonogameSample.System;
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
            World.Load(Content);
            TextureCache.Load(Content);
            Framing.Load();
            GameText.Load(Content);
            player = Player.MakePlayer();
            MovementSystem.Movers.Add(player.GetComponent<MobileComponent>());
            DrawerSystem.Drawers.Add(player.GetComponent<BasicTextureDrawer>());
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputSystem.Update();
            player.GetComponent<PlayerPhysics>().Update();
            MovementSystem.Update();
            Camera.GameCamera.Update(player.GetComponent<MobileComponent>().Center, Window);
            GameText.Update(""+gameTime.TotalGameTime.Ticks);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            World.Draw(_spriteBatch);
            DrawerSystem.Draw(_spriteBatch);
            GameText.Draw(_spriteBatch);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
