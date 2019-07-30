using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;
using SolarOdyssey.System;
using SolarOdyssey.System.Render;
using SolarOdyssey.System.Update;

namespace SolarOdyssey
{
    public class MainGame : Game
    {
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;
        private World _world;
        private EntityFactory _entityFactory;
        private readonly Random _random;
        private OrthographicCamera _camera;

        public MainGame()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _random = new Random();
            Window.AllowUserResizing = true;
        }

        protected override void LoadContent()
        {
            _camera = new OrthographicCamera(new BoxingViewportAdapter(Window, GraphicsDevice, 800, 400));
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _entityFactory = new EntityFactory(_camera);
            _world = new WorldBuilder()
                // Updates
                .AddSystem(new PlayerInputSystem(_entityFactory, Content))
                .AddSystem(new MovementSystem())
                .AddSystem(new ExpirationSystem())
                .AddSystem(new HudSystem(_entityFactory, _camera))
                .AddSystem(new EnemySpawnSystem(_camera, _entityFactory, _random))
                .AddSystem(new CollisionSystem(_entityFactory))
                .AddSystem(new LifeSystem())
                .AddSystem(new InvulnerabilitySystem())
                .AddSystem(new EnemyAISystem(_random, _entityFactory))
                .AddSystem(new CameraSystem(_camera))
                .AddSystem(new DebugSystem())
                
                // Draws
                .AddSystem(new BackgroundSystem(Content, _spriteBatch, _camera))
                .AddSystem(new RenderSystem(_spriteBatch, Content))
                .AddSystem(new CollisionRenderSystem(_spriteBatch))
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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
            GraphicsDevice.Clear(Color.Black);
            _world.Draw(gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}