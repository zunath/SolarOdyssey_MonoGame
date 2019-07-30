using Microsoft.Xna.Framework;

namespace SolarOdyssey.Component
{
    internal class CollisionComponent
    {
        public Rectangle Bounds { get; set; }

        public CollisionComponent(Rectangle bounds)
        {
            Bounds = bounds;
        }
    }
}
