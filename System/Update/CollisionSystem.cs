using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Update
{
    internal class CollisionSystem: EntityUpdateSystem
    {
        private readonly EntityFactory _entityFactory;

        public CollisionSystem(EntityFactory entityFactory) 
            : base(Aspect.All(typeof(CollisionComponent), typeof(Transform2)))
        {
            _entityFactory = entityFactory;
        }
        
        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Update(GameTime gameTime)
        {
            var players = ActiveEntities.Where(x => GetEntity(x).Has<PlayerComponent>()).ToArray();
            var bullets = ActiveEntities.Where(x => GetEntity(x).Has<BulletComponent>()).ToArray();
            var enemies = ActiveEntities.Where(x => GetEntity(x).Has<EnemyComponent>()).ToArray();

            foreach (var enemyID in enemies)
            {
                var enemy = GetEntity(enemyID);
                var enemyCollisionBox = GetCollisionBox(enemy);
                var enemyPosition = enemy.Get<Transform2>().Position;
                var entityList = enemy.Get<LinkedEntityComponent>();

                foreach(var bulletID in bullets)
                {
                    var bullet = GetEntity(bulletID);
                    var bulletComponent = bullet.Get<BulletComponent>();
                    if (bulletComponent.OwnerID == enemyID) continue;

                    var bulletCollisionBox = GetCollisionBox(bullet);

                    if (enemyCollisionBox.Intersects(bulletCollisionBox))
                    {
                        var health = enemy.Get<HealthComponent>();
                        health.Current--;
                        if (health.Current <= 0)
                        {
                            _entityFactory.CreateExplosion(enemyPosition.X, enemyPosition.Y);

                            // Remove all attached entities (life, life bar).
                            foreach (var entityID in entityList.EntityIDs)
                            {
                                GetEntity(entityID).Destroy();
                            }

                            enemy.Destroy();
                        }

                        bullet.Destroy();
                    }
                }
            }

        }

        private static Rectangle GetCollisionBox(Entity entity)
        {
            var transform = entity.Get<Transform2>();
            var collision = entity.Get<CollisionComponent>();
            return new Rectangle(
                (int)(transform.Position.X + collision.Bounds.X),
                (int)(transform.Position.Y + collision.Bounds.Y),
                collision.Bounds.Width,
                collision.Bounds.Height);
        }
    }
}
