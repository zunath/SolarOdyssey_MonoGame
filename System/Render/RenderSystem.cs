using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Render
{
    internal class RenderSystem: EntityDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly Dictionary<string, Texture2D> _textures;
        private readonly ContentManager _content;


        public RenderSystem(SpriteBatch spriteBatch, ContentManager content) 
            : base(Aspect.All(typeof(RenderableComponent), typeof(Transform2)))
        {
            _spriteBatch = spriteBatch;
            _content = content;
            _textures = new Dictionary<string, Texture2D>();
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var entityID in ActiveEntities)
            {
                var entity = GetEntity(entityID);
                var renderable = entity.Get<RenderableComponent>();
                var transform = entity.Get<Transform2>();
                var position = transform.Position;

                if (!_textures.ContainsKey(renderable.FileName))
                    _textures[renderable.FileName] = _content.Load<Texture2D>(renderable.FileName);

                Texture2D texture = _textures[renderable.FileName];
                Vector2 vector = new Vector2(position.X, position.Y);
                SpriteEffects flipEffects = SpriteEffects.None;
                if (renderable.FlipVertical && renderable.FlipHorizontal)
                    flipEffects = SpriteEffects.FlipVertically & SpriteEffects.FlipHorizontally;
                else if (renderable.FlipHorizontal)
                    flipEffects = SpriteEffects.FlipHorizontally;
                else if (renderable.FlipVertical)
                    flipEffects = SpriteEffects.FlipVertically;

                Color color = new Color(Color.White, renderable.Opacity);

                _spriteBatch.Draw(texture, 
                    vector, 
                    null, 
                    color, 
                    0.0f, 
                    Vector2.Zero, 
                    new Vector2(renderable.ScaleX, renderable.ScaleY), 
                    flipEffects, 
                    0.0f);
            }
        }
    }
}
