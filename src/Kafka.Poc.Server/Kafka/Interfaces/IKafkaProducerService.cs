namespace Kafka.Poc.Server.Kafka.Interfaces;

using global::Kafka.PoC.Shared.Interfaces;

internal interface IKafkaProducerService
{
    public Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : IMessage;

    Task PublishAsync<TMessage>(TMessage message, string partitionId, CancellationToken cancellationToken = default)
        where TMessage : IMessage;
}
