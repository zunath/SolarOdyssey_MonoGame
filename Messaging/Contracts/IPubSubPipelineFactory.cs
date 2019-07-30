namespace SolarOdyssey.Messaging.Contracts
{
    public interface IPubSubPipelineFactory
    {
        IPublisher GetPublisher();
        ISubscriber GetSubscriber();
    }
}