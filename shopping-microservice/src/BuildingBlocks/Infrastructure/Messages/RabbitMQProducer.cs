﻿using Contracts.Common.Interfaces;
using Contracts.Messages;
using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.Messages;

public class RabbitMQProducer : IMessageProducer
{
    private readonly ISerializeService _serializeService;

    public RabbitMQProducer(ISerializeService serializeService)
    {
        _serializeService = serializeService;
    }

    public void SendMessage<T>(T message)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672
        };

        var connection = connectionFactory.CreateConnection();

        using var channel = connection.CreateModel();
        channel.QueueDeclare("orders", exclusive: false);

        var json = _serializeService.Serialize(message);

        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
    }
}
