namespace SolarOdyssey.Messaging.Contracts
{
    public interface IPublisher
    {
        void Publish<T>( object sender, T data );
    }
}
