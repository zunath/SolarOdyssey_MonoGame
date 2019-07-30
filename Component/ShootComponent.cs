using System;

namespace SolarOdyssey.Component
{
    internal class ShootComponent
    {
        public TimeSpan Timer { get; set; }
        public TimeSpan Delay { get; set; }

        public ShootComponent(TimeSpan delay)
        {
            Timer = TimeSpan.Zero;
            Delay = delay;
        }
    }
}
