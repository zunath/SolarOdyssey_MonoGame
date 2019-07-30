using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

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

            int x = _random.Next(10, _viewport.Width-10);
            int y = _random.Next(0, _viewport.Height/2);
            _entityFactory.CreateEnemy(x, y);

            _enemyCount++;
        }
    }
}
