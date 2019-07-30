using Microsoft.Xna.Framework;
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
            var health = entity.Get<HealthComponent>();

            foreach(var linkedEntityID in linked.EntityIDs)
            {
                var linkedEntity = GetEntity(linkedEntityID);
                if (!linkedEntity.Has<LifeComponent>()) continue;

                var lifeRenderable = linkedEntity.Get<RenderableComponent>();
                lifeRenderable.ScaleX = 0.48f * ((float) health.Current / (float) health.Maximum);
            }
        }
    }
}
