using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using SolarOdyssey.Component;

namespace SolarOdyssey
{
    internal class EntityFactory
    {
        private readonly OrthographicCamera _camera;
        public World World { get; set; }
        
        public EntityFactory(OrthographicCamera camera)
        {
            _camera = camera;
        }



        public Entity CreatePlayer()
        {
            var entity = World.CreateEntity();
            entity.Attach(new HealthComponent(100, 100));
            entity.Attach(new PhysicsComponent());
            entity.Attach(new Transform2(_camera.BoundingRectangle.Width / 2.1f, _camera.BoundingRectangle.Height - 50));
            entity.Attach(new PlayerComponent());
            entity.Attach(new RenderableComponent("Ship/6b", false, true));
            entity.Attach(new CollisionComponent(new Rectangle(0, 0, 44, 36)){IsVisible = true});
            entity.Attach(new LinkedEntityComponent());
            entity.Attach(new InvulnerabilityComponent(500));
            entity.Attach(new ShootComponent(TimeSpan.FromMilliseconds(100)));

            return entity;
        }

        public Entity CreateBullet(int ownerID)
        {
            var entity = World.CreateEntity();
            entity.Attach(new Transform2());
            entity.Attach(new RenderableComponent("Shoot/1", false, false));
            entity.Attach(new ExpiresComponent(2000));
            entity.Attach(new PhysicsComponent());
            entity.Attach(new CollisionComponent(new Rectangle(0, 0, 16, 16)){ IsVisible = true});
            entity.Attach(new BulletComponent(ownerID));
            entity.Attach(new TouchDamageComponent(1));

            return entity;
        }

        public Entity CreateLifeBar(float x, float y, float scaleX, float scaleY, float opacity = 1.0f)
        {
            var entity = World.CreateEntity();
            entity.Attach(new Transform2(x, y));
            entity.Attach(new RenderableComponent("HUD/HealthBar", false, false) { ScaleX = scaleX, ScaleY = scaleY, Opacity = opacity});
            entity.Attach(new LifeBarComponent(0, 45));

            return entity;
        }

        public Entity CreateLife(float x, float y, float scaleX, float scaleY, float opacity = 1.0f)
        {
            var entity = World.CreateEntity();
            entity.Attach(new Transform2(x, y));
            entity.Attach(new RenderableComponent("HUD/HealthBarColor", false, false) { ScaleX = scaleX, ScaleY = scaleY, Opacity = opacity});
            entity.Attach(new LifeComponent(5, 46));

            return entity;
        }

        public Entity CreateEnemy(float positionX, float positionY)
        {
            var entity = World.CreateEntity();
            entity.Attach(new Transform2(positionX, positionY));
            entity.Attach(new HealthComponent(10, 10));
            entity.Attach(new PhysicsComponent());
            entity.Attach(new EnemyComponent());
            entity.Attach(new RenderableComponent("Ship/3", false, false));
            entity.Attach(new CollisionComponent(new Rectangle(0, 0, 46, 45)){IsVisible = true});
            entity.Attach(new LinkedEntityComponent());
            entity.Attach(new TouchDamageComponent(2));
            entity.Attach(new ShootComponent(TimeSpan.FromMilliseconds(500)));

            return entity;
        }

        public Entity CreateExplosion(float positionX, float positionY)
        {
            var entity = World.CreateEntity();
            entity.Attach(new Transform2(positionX, positionY));
            entity.Attach(new RenderableComponent("Fx/Explosion1", false, false));
            entity.Attach(new ExpiresComponent(500));

            return entity;
        }
    }
}
