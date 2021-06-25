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
    public class MessageProducerScopeFactory : IMessageProducerScopeFactory
    {
        private readonly IConnectionFactory _connectionFactory;
        public MessageProducerScopeFactory(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public IMessageProducerScope Open(MessageScopeSettings messageScopeSettings)
        {
            return new MessageProducerScope(_connectionFactory, messageScopeSettings);
        }
    }
}
