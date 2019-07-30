using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Update
{
    internal class LifeSystem: EntityProcessingSystem
    {
        public LifeSystem() 
            : base(Aspect
                .All(typeof(HealthComponent), typeof(RenderableComponent))
                .Exclude(typeof(PlayerComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var entity = GetEntity(entityId);
            var linked = entity.Get<LinkedEntityComponent>();
            var entityHealth = entity.Get<HealthComponent>();
            var entityTransform = entity.Get<Transform2>();

            foreach(var linkedEntityID in linked.EntityIDs)
            {
                var linkedEntity = GetEntity(linkedEntityID);
                if (linkedEntity.Has<LifeComponent>())
                {
                    var transform = linkedEntity.Get<Transform2>();
                    var life = linkedEntity.Get<LifeComponent>();
                    var lifeRenderable = linkedEntity.Get<RenderableComponent>();
                    lifeRenderable.ScaleX = 0.48f * ((float) entityHealth.Current / (float) entityHealth.Maximum);
                    transform.Position = new Vector2(entityTransform.Position.X + life.OffsetX, entityTransform.Position.Y + life.OffsetY);
                }
                else if(linkedEntity.Has<LifeBarComponent>())
                {
                    var transform = linkedEntity.Get<Transform2>();
                    var lifeBar = linkedEntity.Get<LifeBarComponent>();
                    transform.Position = new Vector2(entityTransform.Position.X, entityTransform.Position.Y);
                    transform.Position = new Vector2(entityTransform.Position.X + lifeBar.OffsetX, entityTransform.Position.Y + lifeBar.OffsetY);
                }
            }
        }
    }
}
