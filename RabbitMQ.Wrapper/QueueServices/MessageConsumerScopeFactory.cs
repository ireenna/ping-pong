using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Wrapper.Interfaces;
using RabbitMQ.Wrapper.Models;

namespace RabbitMQ.Wrapper.QueueServices
{
    public class MessageConsumerScopeFactory : IMessageConsumerScopeFactory
    {
        private readonly IConnectionFactory _connectionFactory;
        public MessageConsumerScopeFactory(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public IMessageConsumerScope Open(MessageScopeSettings messageScopeSettings)
        {
            return new MessageConsumerScope(_connectionFactory, messageScopeSettings);
        }
        public IMessageConsumerScope Connect(MessageScopeSettings messageScopeSettings)
        {
            var mqConsumerScope = Open(messageScopeSettings);
            mqConsumerScope.MessageConsumer.Connect();

            return mqConsumerScope;
        }
    }
}
