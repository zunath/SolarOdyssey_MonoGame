using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Update
{
    internal class ExpirationSystem: EntityProcessingSystem
    {
        public ExpirationSystem() 
            : base(Aspect.All(typeof(ExpiresComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var entity = GetEntity(entityId);
            var expires = entity.Get<ExpiresComponent>();

            expires.LifeTime -= gameTime.ElapsedGameTime;
            if (expires.LifeTime > TimeSpan.Zero)
                return;

            entity.Destroy();
            expires.LifeTime = TimeSpan.Zero;
        }
    }
}
