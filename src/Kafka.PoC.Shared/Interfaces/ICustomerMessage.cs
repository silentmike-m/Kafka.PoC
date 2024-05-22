namespace Kafka.PoC.Shared.Interfaces;

using Kafka.PoC.Shared.Constants;

public interface ICustomerMessage : IMessage
{
    public string FirstName { get; }
    public Guid Id { get; }
    public string LastName { get; }
    string IMessage.TopicName => TopicNames.CUSTOMER_TOPIC_NAME;
}
