using SolarOdyssey.Messaging.Contracts;

namespace SolarOdyssey.Messaging
{
    public class PubSubPipelineFactory : IPubSubPipelineFactory
    {
        private readonly MessageHub hub;

        public PubSubPipelineFactory()
        {
            hub = new MessageHub();
        }


        public IPublisher GetPublisher() => new Publisher( hub );

        public ISubscriber GetSubscriber() => new Subscriber( hub );
    }
}
