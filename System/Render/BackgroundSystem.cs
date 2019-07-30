using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace SolarOdyssey.System.Render
{
    internal class BackgroundSystem: DrawSystem
    {
        private readonly ContentManager _content;
        private Texture2D _texture;
        private readonly SpriteBatch _spriteBatch;
        private readonly OrthographicCamera _camera;

        public BackgroundSystem(ContentManager content, SpriteBatch spriteBatch, OrthographicCamera camera)
        {
            _content = content;
            _spriteBatch = spriteBatch;
            _camera = camera;
        }

        public override void Initialize(World world)
        {
            base.Initialize(world);

            _texture = _content.Load<Texture2D>("Background/Stars");
        }

        public override void Draw(GameTime gameTime)
        {
            int backgroundWidth = _texture.Width;
            int backgroundHeight = _texture.Height;

            int xOffset = (int)(_camera.Position.X / backgroundWidth);
            int yOffset = (int) (_camera.Position.Y / backgroundHeight);

            for (int x = xOffset-1; x <= xOffset+2; x++)
            {
                for (int y = yOffset-1; y <= yOffset+2; y++)
                {
                    var position = new Vector2(backgroundWidth * x, backgroundHeight * y);
                    
                    _spriteBatch?.Draw(
                        _texture, 
                        position, 
                        null, 
                        Color.White, 
                        0.0f, 
                        Vector2.Zero, 
                        new Vector2(1.0f, 1.0f), 
                        SpriteEffects.None, 
                        0.0f );
                }
            }

        }
    }
}
