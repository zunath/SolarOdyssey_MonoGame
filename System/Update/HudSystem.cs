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
        private readonly Viewport _viewport;

        public HudSystem(
            EntityFactory entityFactory,
            Viewport viewport) 
            : base(Aspect.All(typeof(PlayerComponent)))
        {
            _entityFactory = entityFactory;
            _viewport = viewport;
        }
        
        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            if (_lifeBar == null)
                _lifeBar = _entityFactory.CreateLifeBar(10, _viewport.Height-20, 8.0f, 0.6f);

            var player = GetEntity(entityId);
            var health = player.Get<HealthComponent>();

            if (_life == null)
                _life = _entityFactory.CreateLife(80, _viewport.Height-16, 8.0f, 0.6f);

            var lifeRenderable = _life.Get<RenderableComponent>();
            lifeRenderable.ScaleX = 8.0f * ((float) health.Current / (float) health.Maximum);
        }
    }
}
