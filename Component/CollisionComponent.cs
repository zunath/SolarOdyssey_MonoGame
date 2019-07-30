using Microsoft.Xna.Framework;

namespace SolarOdyssey.Component
{
    internal class CollisionComponent
    {
        public Rectangle Bounds { get; set; }
        public bool IsVisible { get; set; }

        public CollisionComponent(Rectangle bounds)
        {
            Bounds = bounds;
            IsVisible = false;
        }
    }
}
