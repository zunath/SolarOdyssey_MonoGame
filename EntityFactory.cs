using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using SolarOdyssey.Component;

namespace SolarOdyssey
{
    internal class EntityFactory
    {
        private readonly Viewport _viewport;
        public World World { get; set; }

        public EntityFactory(Viewport viewport)
        {
            _viewport = viewport;
        }

        public Entity CreatePlayer()
        {
            var entity = World.CreateEntity();
            entity.Attach(new HealthComponent(100, 100));
            entity.Attach(new PhysicsComponent());
            entity.Attach(new Transform2(_viewport.Width / 2.1f, _viewport.Height - 50));
            entity.Attach(new PlayerComponent());
            entity.Attach(new RenderableComponent("Ship/6b", false, true));

            return entity;
        }

        public Entity CreateBullet()
        {
            var entity = World.CreateEntity();
            entity.Attach(new Transform2());
            entity.Attach(new RenderableComponent("Shoot/1", false, false));
            entity.Attach(new ExpiresComponent{LifeTime = TimeSpan.FromMilliseconds(2000)});
            entity.Attach(new PhysicsComponent());

            return entity;
        }

        public Entity CreateLifeBar(float x, float y)
        {
            var entity = World.CreateEntity();
            entity.Attach(new Transform2(x, y));
            entity.Attach(new RenderableComponent("HUD/HealthBar", false, false) { ScaleX = 8.0f, ScaleY = 0.6f});

            return entity;
        }

        public Entity CreateLife(float x, float y)
        {
            var entity = World.CreateEntity();
            entity.Attach(new Transform2(x, y));
            entity.Attach(new RenderableComponent("HUD/HealthBarColor", false, false) { ScaleX = 8.0f, ScaleY = 0.6f});

            return entity;
        }
    }
}
