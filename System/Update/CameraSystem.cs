using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Update
{
    internal class CameraSystem: EntityProcessingSystem
    {
        private readonly OrthographicCamera _camera;

        public CameraSystem(OrthographicCamera camera) 
            : base(Aspect.All(typeof(PlayerComponent)))
        {
            _camera = camera;
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var player = GetEntity(entityId);
            var position = player.Get<Transform2>();
            _camera.LookAt(position.Position);

        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }
    }
}
