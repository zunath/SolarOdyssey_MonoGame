using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Update
{
    internal class HudSystem: EntityProcessingSystem
    {
        private Entity _lifeBar;
        private Entity _life;
        private readonly EntityFactory _entityFactory;
        private readonly OrthographicCamera _camera;

        public HudSystem(
            EntityFactory entityFactory,
            OrthographicCamera camera) 
            : base(Aspect.All(typeof(PlayerComponent)))
        {
            _entityFactory = entityFactory;
            _camera = camera;
        }
        
        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            if (_lifeBar == null)
                _lifeBar = _entityFactory.CreateLifeBar(_camera.BoundingRectangle.X + 10, _camera.BoundingRectangle.Height-20, 8.0f, 0.6f);
            if (_life == null)
                _life = _entityFactory.CreateLife(_camera.BoundingRectangle.X + 80, _camera.BoundingRectangle.Height-16, 8.0f, 0.6f);
            
            var player = GetEntity(entityId);
            var playerHealth = player.Get<HealthComponent>();
            var playerPhysics = player.Get<PhysicsComponent>();
            var lifeRenderable = _life.Get<RenderableComponent>();
            lifeRenderable.ScaleX = 8.0f * ((float) playerHealth.Current / (float) playerHealth.Maximum);

            _lifeBar.Get<Transform2>().Position = new Vector2(_camera.BoundingRectangle.Left + 10 + playerPhysics.SpeedX, _camera.BoundingRectangle.Bottom-20 + playerPhysics.SpeedY);
            _life.Get<Transform2>().Position = new Vector2(_camera.BoundingRectangle.Left + 80 + playerPhysics.SpeedX, _camera.BoundingRectangle.Bottom-16 + playerPhysics.SpeedY);
        }
    }
}
