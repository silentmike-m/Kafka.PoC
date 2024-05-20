namespace Kafka.PoC.Shared.Interfaces;

public interface ICarMessage : IMessage
{
    public decimal EngineSize { get; }
    public Guid Id { get; }
    public string Manufacturer { get; }
    public string Model { get; }
    string IMessage.TopicName => "CAR-TOPIC";
}
