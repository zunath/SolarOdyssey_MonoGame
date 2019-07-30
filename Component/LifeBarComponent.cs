namespace SolarOdyssey.Component
{
    internal class LifeBarComponent
    {
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }

        public LifeBarComponent(int offsetX, int offsetY)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
        }
    }
}
