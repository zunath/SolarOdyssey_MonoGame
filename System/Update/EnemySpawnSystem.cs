using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Update
{
    internal class EnemySpawnSystem: UpdateSystem
    {
        private readonly Viewport _viewport;
        private readonly Random _random;
        private int _enemyCount;
        private readonly EntityFactory _entityFactory;

        public EnemySpawnSystem(Viewport viewport, EntityFactory entityFactory)
        {
            _viewport = viewport;
            _random = new Random();
            _entityFactory = entityFactory;
        }

        public override void Update(GameTime gameTime)
        {
            if (_enemyCount >= 5) return;

            var x = _random.Next(10, _viewport.Width-10);
            var y = _random.Next(0, _viewport.Height/2);
            var enemy = _entityFactory.CreateEnemy(x, y);
            var lifeBar = _entityFactory.CreateLifeBar(x, y + 45, 0.48f, 0.3f, 0.2f);
            var life = _entityFactory.CreateLife(x+5, y + 46, 0.48f, 0.3f, 0.2f);

            var entityList = enemy.Get<LinkedEntityComponent>();
            entityList.EntityIDs.Add(lifeBar.Id);
            entityList.EntityIDs.Add(life.Id);

            _enemyCount++;
        }
    }
}
