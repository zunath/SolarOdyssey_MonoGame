using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Update
{
    internal class EnemyAISystem: EntityProcessingSystem
    {
        private readonly Random _random;
        private readonly EntityFactory _entityFactory;

        public EnemyAISystem(Random random, EntityFactory entityFactory) 
            : base(Aspect.All(typeof(EnemyComponent)))
        {
            _random = random;
            _entityFactory = entityFactory;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var enemy = GetEntity(entityId);
            var comp = enemy.Get<EnemyComponent>();
            var enemyTransform = enemy.Get<Transform2>();
            var enemyShoot = enemy.Get<ShootComponent>();

            enemyShoot.Timer += gameTime.ElapsedGameTime;
            if (enemyShoot.Timer >= enemyShoot.Delay)
            {
                enemyShoot.Timer = TimeSpan.Zero;

                if(_random.Next(100) <= 40)
                {
                    var bullet = _entityFactory.CreateBullet(enemy.Id);
                    bullet.Get<PhysicsComponent>().SpeedY = 5;
                    bullet.Get<Transform2>().Position = enemyTransform.Position + new Vector2(13, 18);
                    bullet.Get<TouchDamageComponent>().Amount = 5;
                }
            }

            comp.CurrentActionTime += gameTime.ElapsedGameTime;
            // Still performing this action.
            if (comp.CurrentActionTime < comp.ActionLength)
            {
                comp.CurrentAction();
                return;
            }


            // Time for a new action.
            comp.CurrentActionTime = TimeSpan.Zero;
            int actionID = _random.Next(3);

            switch (actionID)
            {
                // Move Left
                case 0:
                    comp.ActionLength = TimeSpan.FromMilliseconds(3000);
                    comp.CurrentAction = () =>
                    {
                        var physics = enemy.Get<PhysicsComponent>();
                        physics.SpeedX = -1;
                    };
                    break;

                // Move Right
                case 1:
                    comp.ActionLength = TimeSpan.FromMilliseconds(3000);
                    comp.CurrentAction = () =>
                    {
                        var physics = enemy.Get<PhysicsComponent>();
                        physics.SpeedX = 1;
                    };
                    break;

                // Do nothing.
                case 2:
                    comp.ActionLength = TimeSpan.FromMilliseconds(1500);
                    comp.CurrentAction = () =>
                    {
                        var physics = enemy.Get<PhysicsComponent>();
                        physics.SpeedX = 0;
                        physics.SpeedY = 0;
                    };
                    break;
            }

            
        }
    }
}
