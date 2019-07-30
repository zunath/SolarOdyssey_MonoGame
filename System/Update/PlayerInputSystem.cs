﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SolarOdyssey.Component;

namespace SolarOdyssey.System.Update
{
    internal class PlayerInputSystem: EntityProcessingSystem
    {
        private readonly EntityFactory _entityFactory;
        private TimeSpan _gunTimer;
        private readonly TimeSpan _gunDelay;

        public PlayerInputSystem(EntityFactory entityFactory) 
            : base(Aspect.All(typeof(PlayerComponent), typeof(Transform2)))
        {
            _entityFactory = entityFactory;
            _gunDelay = TimeSpan.FromMilliseconds(100);
            _gunTimer = TimeSpan.Zero;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            Movement(entityId);
            Actions(gameTime, entityId);
        }

        private void Movement(int entityId)
        {
            var keyboard = Keyboard.GetState();
            var entity = GetEntity(entityId);
            var physics = entity.Get<PhysicsComponent>();
            physics.SpeedX = 0;
            physics.SpeedY = 0;

            if (keyboard.IsKeyDown(Keys.D))
                physics.SpeedX = 5;
            if (keyboard.IsKeyDown(Keys.A))
                physics.SpeedX = -5;
            if (keyboard.IsKeyDown(Keys.W))
                physics.SpeedY = -5;
            if (keyboard.IsKeyDown(Keys.S))
                physics.SpeedY = 5;
        }

        private void Actions(GameTime gameTime, int entityId)
        {
            var keyboard = Keyboard.GetState();
            var entity = GetEntity(entityId);
            var transform = entity.Get<Transform2>();

            if (keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Enter))
            {
                _gunTimer += gameTime.ElapsedGameTime;
                if (_gunTimer >= _gunDelay)
                {
                    _gunTimer = TimeSpan.Zero;

                    var bullet = _entityFactory.CreateBullet();
                    var bulletTransform = bullet.Get<Transform2>();
                    var bulletPhysics = bullet.Get<PhysicsComponent>();
                    bulletTransform.Position = transform.WorldPosition + new Vector2(13, -18);
                    bulletPhysics.SpeedY = -10;
                }
            }

        }
    }
}