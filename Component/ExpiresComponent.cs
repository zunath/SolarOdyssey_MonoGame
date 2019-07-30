using System;

namespace SolarOdyssey.Component
{
    internal class ExpiresComponent
    {
        public bool IsExpired => LifeTime <= TimeSpan.Zero;
        public TimeSpan LifeTime { get; set; }

        public ExpiresComponent(TimeSpan lifetime)
        {
            LifeTime = lifetime;
        }

        public ExpiresComponent(int milliseconds)
        {
            LifeTime = TimeSpan.FromMilliseconds(milliseconds);
        }
    }
}
