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

        public EnemyAISystem(Random random) 
            : base(Aspect.All(typeof(EnemyComponent)))
        {
            _random = random;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var enemy = GetEntity(entityId);
            var comp = enemy.Get<EnemyComponent>();

            comp.CurrentActionTime += gameTime.ElapsedGameTime;
            // Still performing this action.
            if (comp.CurrentActionTime < comp.ActionLength)
            {
                comp.CurrentAction();
                return;
            }

            // Time for a new action.
            comp.CurrentActionTime = TimeSpan.Zero;
            int actionID = _random.Next(4);

            Console.WriteLine("actionID = " + actionID);

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

                // Shoot
                case 2:
                    comp.ActionLength = TimeSpan.FromMilliseconds(1000);
                    comp.CurrentAction = () =>
                    {

                    };
                    break;
                // Do nothing.
                case 3:
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
