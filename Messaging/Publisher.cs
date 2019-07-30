using SolarOdyssey.Messaging.Contracts;

namespace SolarOdyssey.Messaging
{
    public class Publisher : IPublisher
    {
        private readonly MessageHub hub;

        public Publisher( MessageHub hub )
        {
            this.hub = hub;
        }

        public void Publish<T>( object sender, T data ) => hub.Publish( sender, data );
    }
}
