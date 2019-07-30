namespace SolarOdyssey.Component
{
    internal class BulletComponent
    {
        public int OwnerID { get; set; }

        public BulletComponent(int ownerID)
        {
            OwnerID = ownerID;
        }
    }
}
