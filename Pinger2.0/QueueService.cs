using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Wrapper.Interfaces;
using RabbitMQ.Wrapper.Models;

namespace Pinger2._0
{
    public class QueueService
    {
        private readonly IMessageProducerScope _messageProducerScope;
        private readonly IMessageConsumerScope _messageConsumerScope;

        public QueueService(
            IMessageProducerScopeFactory messageProducerScopeFactory,
            IMessageConsumerScopeFactory messageConsumerScopeFactory)
        {
            _messageProducerScope = messageProducerScopeFactory.Open(new MessageScopeSettings
            {
                ExchangeName = "ServerExchange",
                ExchangeType = ExchangeType.Topic,
                QueueName = "ping_queue",
                RoutingKey = "topic.queue"
            });
            _messageConsumerScope = messageConsumerScopeFactory.Connect(new MessageScopeSettings
            {
                ExchangeName = "ClientExchange",
                ExchangeType = ExchangeType.Direct,
                QueueName = "pong_queue",
                RoutingKey = "response"
            });
            _messageConsumerScope.MessageConsumer.Received += GetValue;
        }
        public bool Ping(string value)
        {
            try
            {
                _messageProducerScope.MessageProducer.SendMessageToQueue(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void GetValue(object sender, RabbitMQ.Client.Events.BasicDeliverEventArgs args)
        {
            var value = Encoding.UTF8.GetString(args.Body);
            Console.WriteLine($"PINGER gets: {value}");
            Thread.Sleep(2500);
            _messageConsumerScope.MessageConsumer.SetAcknowledge(args.DeliveryTag, true);

            Console.WriteLine("Ping " + DateTime.Now.ToString());
            Ping("Ping " + DateTime.Now.ToString());
        }
    }
}
