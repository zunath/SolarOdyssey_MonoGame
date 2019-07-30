using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities.Systems;

namespace SolarOdyssey.System.Update
{
    internal class DebugSystem: UpdateSystem
    {
        private KeyboardState _lastState;
        public static bool IsDebugActive { get; private set; }

        public override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.LeftControl) && state.IsKeyDown(Keys.K) &&
                !(_lastState.IsKeyDown(Keys.LeftControl) && _lastState.IsKeyDown(Keys.K)))
            {
                IsDebugActive = !IsDebugActive;
            }

            _lastState = state;
        }
    }
}
