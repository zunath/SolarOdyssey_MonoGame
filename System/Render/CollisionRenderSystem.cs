using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Render
{
    internal class CollisionRenderSystem: EntityDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;

        public CollisionRenderSystem(SpriteBatch spriteBatch) 
            : base(Aspect.All(typeof(CollisionComponent), typeof(Transform2)))
        {
            _spriteBatch = spriteBatch;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var entityID in ActiveEntities)
            {
                var entity = GetEntity(entityID);
                var collision = entity.Get<CollisionComponent>();

                if(!collision.IsVisible) continue;

                var transform = entity.Get<Transform2>();
                var drawPosition = new Rectangle(
                    (int)transform.Position.X + collision.Bounds.X,
                    (int)transform.Position.Y + collision.Bounds.Y,
                    collision.Bounds.Width,
                    collision.Bounds.Height);

                _spriteBatch.DrawRectangle(drawPosition, Color.Red);
            }
        }
    }
}
