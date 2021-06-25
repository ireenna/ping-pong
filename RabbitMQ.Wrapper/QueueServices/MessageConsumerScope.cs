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
    public class MessageConsumerScope :IMessageConsumerScope
    {
        private readonly MessageScopeSettings _messageScopeSettings;
        private readonly Lazy<IMessageQueue> _messageQueueLazy;
        private readonly Lazy<IMessageConsumer> _messageConsumerLazy;

        private readonly IConnectionFactory _conectionFactory;

        public MessageConsumerScope(IConnectionFactory connectionFactory, MessageScopeSettings messageScopeSettings)
        {
            _conectionFactory = connectionFactory;
            _messageScopeSettings = messageScopeSettings;

            _messageQueueLazy = new Lazy<IMessageQueue>(CreateMessageQueue);
            _messageConsumerLazy = new Lazy<IMessageConsumer>(CreateMessageConsumer);
        }

        public IMessageConsumer MessageConsumer => _messageConsumerLazy.Value;

        private IMessageQueue MessageQueue => _messageQueueLazy.Value;

        private IMessageQueue CreateMessageQueue()
        {
            return new MessageQueue(_conectionFactory, _messageScopeSettings);
        }

        private IMessageConsumer CreateMessageConsumer()
        {
            return new MessageConsumer(new MessageConsumerSettings
            {
                Channel = MessageQueue.Channel,
                QueueName = _messageScopeSettings.QueueName
            });
        }

        public void Dispose()
        {
            MessageQueue?.Dispose();
        }
    }
}
