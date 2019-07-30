﻿namespace SolarOdyssey.Component
{
    internal class LifeComponent
    {
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }

        public LifeComponent(int offsetX, int offsetY)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
        }
    }
}
