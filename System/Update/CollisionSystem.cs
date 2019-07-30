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
            ProcessEnemyCollisions();
            ProcessPlayerCollisions();
        }

        private void ProcessEnemyCollisions()
        {
            var bullets = ActiveEntities.Where(x => GetEntity(x).Has<BulletComponent>()).ToList();
            var enemies = ActiveEntities.Where(x => GetEntity(x).Has<EnemyComponent>()).ToList();

            for(int enemyIndex = enemies.Count-1; enemyIndex >= 0; enemyIndex--)
            {
                var enemyID = enemies[enemyIndex];
                var enemy = GetEntity(enemyID);
                var enemyCollisionBox = GetCollisionBox(enemy);
                var enemyPosition = enemy.Get<Transform2>().Position;
                var enemyHealth = enemy.Get<HealthComponent>();
                var enemyLinkedEntities = enemy.Get<LinkedEntityComponent>();

                for(int bulletIndex = bullets.Count-1; bulletIndex >= 0; bulletIndex--)
                {
                    var bulletID = bullets[bulletIndex];
                    var bullet = GetEntity(bulletID);
                    var bulletComponent = bullet.Get<BulletComponent>();
                    if (bulletComponent.OwnerID == enemyID) continue;

                    var bulletCollisionBox = GetCollisionBox(bullet);
                    var bulletTouchDamage = bullet.Get<TouchDamageComponent>();

                    if (enemyCollisionBox.Intersects(bulletCollisionBox))
                    {
                        bullet.Destroy();
                        bullets.RemoveAt(bulletIndex);

                        enemyHealth.Current -= bulletTouchDamage.Amount;
                        if (enemyHealth.Current <= 0)
                        {
                            _entityFactory.CreateExplosion(enemyPosition.X, enemyPosition.Y);

                            // Remove all linked entities (life, life bar, etc).
                            foreach (var entityID in enemyLinkedEntities.EntityIDs)
                            {
                                GetEntity(entityID).Destroy();
                            }

                            enemy.Destroy();
                            enemies.RemoveAt(enemyIndex);
                        }

                    }
                }
            }
        }

        private void ProcessPlayerCollisions()
        {
            var players = ActiveEntities.Where(x => GetEntity(x).Has<PlayerComponent>()).ToArray();
            var bullets = ActiveEntities.Where(x => GetEntity(x).Has<BulletComponent>()).ToArray();
            var enemies = ActiveEntities.Where(x => GetEntity(x).Has<EnemyComponent>()).ToArray();

            foreach (var playerID in players)
            {
                var player = GetEntity(playerID);
                var playerCollisionBox = GetCollisionBox(player);
                var playerHealth = player.Get<HealthComponent>();
                var playerInvulnerability = player.Get<InvulnerabilityComponent>();
                if (playerInvulnerability.IsInvulnerable) continue;

                foreach (var enemyID in enemies)
                {
                    var enemy = GetEntity(enemyID);
                    var enemyCollisionBox = GetCollisionBox(enemy);
                    var enemyTouchDamage = enemy.Get<TouchDamageComponent>();

                    if (playerCollisionBox.Intersects(enemyCollisionBox))
                    {
                        playerHealth.Current -= enemyTouchDamage.Amount;
                        playerInvulnerability.Current = TimeSpan.Zero;
                    }
                }

                foreach (var bulletID in bullets)
                {
                    var bullet = GetEntity(bulletID);
                    if (bullet.Get<BulletComponent>().OwnerID == playerID) continue;


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
