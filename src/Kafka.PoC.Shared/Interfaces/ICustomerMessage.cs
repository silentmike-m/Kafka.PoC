namespace Kafka.PoC.Shared.Interfaces;

public interface ICustomerMessage : IMessage
{
    public string FirstName { get; }
    public Guid Id { get; }
    public string LastName { get; }
    string IMessage.TopicName => "CUSTOMER-TOPIC";
}
