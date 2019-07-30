using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace SolarOdyssey.System.Render
{
    internal class BackgroundSystem: DrawSystem
    {
        private readonly ContentManager _content;
        private Texture2D _texture;
        private readonly SpriteBatch _spriteBatch;

        public BackgroundSystem(ContentManager content, SpriteBatch spriteBatch)
        {
            _content = content;
            _spriteBatch = spriteBatch;
        }

        public override void Initialize(World world)
        {
            base.Initialize(world);

            _texture = _content.Load<Texture2D>("Background/5");
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch?.Draw(_texture, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, new Vector2(2.0f, 2.0f), SpriteEffects.None, 0.0f );
        }
    }
}
