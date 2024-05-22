namespace Kafka.PoC.Shared.Interfaces;

using Kafka.PoC.Shared.Constants;

public interface ICarMessage : IMessage
{
    public decimal EngineSize { get; }
    public Guid Id { get; }
    public string Manufacturer { get; }
    public string Model { get; }
    string IMessage.TopicName => TopicNames.CAR_TOPIC_NAME;
}
