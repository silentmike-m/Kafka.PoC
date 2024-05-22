﻿namespace Kafka.Poc.Client.Cars.Kafka.Extensions;

using Confluent.Kafka;

internal static class ClientConfigExtensions
{
    public static void FillConfig(this ClientConfig clientConfig, KafkaOptions options)
    {
        clientConfig.BootstrapServers = options.BootstrapServers;
        clientConfig.ClientId = options.ClientId;
        clientConfig.SecurityProtocol = SecurityProtocol.Plaintext;
        clientConfig.StatisticsIntervalMs = options.StatisticsIntervalInMilliSeconds;

        if (options.UseSaslSsl)
        {
            clientConfig.SaslMechanism = options.SaslMechanism;
            clientConfig.SaslPassword = options.SaslPassword;
            clientConfig.SaslUsername = options.SaslUserName;
            clientConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
        }
    }
}
