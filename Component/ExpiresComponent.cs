using System;

namespace SolarOdyssey.Component
{
    internal class ExpiresComponent
    {
        public bool IsExpired => LifeTime <= TimeSpan.Zero;
        public TimeSpan LifeTime { get; set; }
    }
}
