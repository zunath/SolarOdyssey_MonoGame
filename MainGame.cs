using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using SolarOdyssey.System;
using SolarOdyssey.System.Render;
using SolarOdyssey.System.Update;

namespace SolarOdyssey
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;
        private World _world;
        private EntityFactory _entityFactory;

        public MainGame()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _entityFactory = new EntityFactory(GraphicsDevice.Viewport);
            _world = new WorldBuilder()
                .AddSystem(new BackgroundSystem(Content, _spriteBatch))
                .AddSystem(new RenderSystem(_spriteBatch, Content))
                .AddSystem(new PlayerInputSystem(_entityFactory))
                .AddSystem(new MovementSystem())
                .AddSystem(new ExpirationSystem())
                .Build();
            _entityFactory.World = _world;

            _entityFactory.CreatePlayer();
        }

        protected override void UnloadContent()
        {
            _spriteBatch.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            _world.Update(gameTime);

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _world.Draw(gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}