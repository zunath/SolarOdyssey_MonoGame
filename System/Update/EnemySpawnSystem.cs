using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Update
{
    internal class EnemySpawnSystem: UpdateSystem
    {
        private readonly OrthographicCamera _camera;
        private readonly Random _random;
        private int _enemyCount;
        private readonly EntityFactory _entityFactory;

        public EnemySpawnSystem(OrthographicCamera camera, EntityFactory entityFactory, Random random)
        {
            _camera = camera;
            _entityFactory = entityFactory;
            _random = random;
        }

        public override void Update(GameTime gameTime)
        {
            if (_enemyCount >= 5) return;

            var x = _random.Next(10, (int)(_camera.BoundingRectangle.Width-10));
            var y = _random.Next(0, (int)(_camera.BoundingRectangle.Height/2));
            var enemy = _entityFactory.CreateEnemy(x, y);
            var lifeBar = _entityFactory.CreateLifeBar(x, y + 45, 0.48f, 0.3f, 0.5f);
            var life = _entityFactory.CreateLife(x+5, y + 46, 0.48f, 0.3f, 0.5f);

            var entityList = enemy.Get<LinkedEntityComponent>();
            entityList.EntityIDs.Add(lifeBar.Id);
            entityList.EntityIDs.Add(life.Id);

            _enemyCount++;
        }
    }
}
