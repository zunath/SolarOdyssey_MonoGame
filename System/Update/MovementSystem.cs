using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Update
{
    internal class MovementSystem: EntityProcessingSystem
    {
        public MovementSystem() 
            : base(Aspect.All(typeof(PhysicsComponent), typeof(Transform2)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var entity = GetEntity(entityId);
            var transform = entity.Get<Transform2>();
            var physics = entity.Get<PhysicsComponent>();
            transform.Position = new Vector2(
                transform.Position.X + physics.SpeedX,
                transform.Position.Y + physics.SpeedY);
        }
    }
}
