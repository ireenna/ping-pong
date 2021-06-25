using System;
using RabbitMQ.Client;
using RabbitMQ.Wrapper.Interfaces;
using RabbitMQ.Wrapper.Models;

namespace RabbitMQ.Wrapper
{
    public class MessageQueue : IMessageQueue
    {
        public readonly IConnection _connection;
        public MessageQueue(IConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection();
            Channel = _connection.CreateModel();
        }
        public MessageQueue(IConnectionFactory connectionFactory, MessageScopeSettings messageScopeSettings):this(connectionFactory)
        {
            DeclareExchange(messageScopeSettings.ExchangeName, messageScopeSettings.ExchangeType);
            if(messageScopeSettings.QueueName != null)
            {
                BindQueue(messageScopeSettings.ExchangeName, messageScopeSettings.RoutingKey, messageScopeSettings.QueueName);
            }
        }
        public IModel Channel { get; set; }
        public void DeclareExchange(string exchangeName, string exchangeType)
        {
            Channel.ExchangeDeclare(exchangeName, exchangeType ?? string.Empty);
        }
        public void BindQueue(string exchangeName, string routingKey, string queueName)
        {
            Channel.QueueDeclare(queueName, true, false, false);
            Channel.QueueBind(queueName, exchangeName,routingKey);
        }
        public void Dispose()
        {
            Channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
