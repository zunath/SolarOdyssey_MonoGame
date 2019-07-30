using System;

namespace SolarOdyssey.Component
{
    internal class EnemyComponent
    {
        public TimeSpan ActionLength { get; set; }
        public TimeSpan CurrentActionTime { get; set; }
        public Action CurrentAction { get; set; }
    }
}
