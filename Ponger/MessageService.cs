using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Wrapper.Interfaces;
using RabbitMQ.Wrapper.Models;

namespace Ponger
{
    public class MessageService
    {
        private readonly IMessageProducerScope _messageProducerScope;
        private readonly IMessageConsumerScope _messageConsumerScope;

        public MessageService(
            IMessageProducerScopeFactory messageProducerScopeFactory,
            IMessageConsumerScopeFactory messageConsumerScopeFactory)
        {
            _messageConsumerScope = messageConsumerScopeFactory.Connect(new MessageScopeSettings
            {
                ExchangeName = "ServerExchange",
                ExchangeType = ExchangeType.Topic,
                QueueName = "ping_queue",
                RoutingKey = "*.queue.#"
            });

            _messageConsumerScope.MessageConsumer.Received += MessageReceived;

            _messageProducerScope = messageProducerScopeFactory.Open(new MessageScopeSettings
            {
                ExchangeName = "ClientExchange",
                ExchangeType = ExchangeType.Direct,
                QueueName = "pong_queue",
                RoutingKey = "response"
            });
            
        }
        public void Run()
        {
            Console.WriteLine("Service run");
        }
        private void MessageReceived(object sender, BasicDeliverEventArgs args)
        {
            var processed = false;
            try
            {
                var value = Encoding.UTF8.GetString(args.Body);
                Console.WriteLine($"PONGER gets {value}");
                Thread.Sleep(2500);
                SendSuccessfulState();
                processed = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                processed = false;
            }
            finally
            {
                _messageConsumerScope.MessageConsumer.SetAcknowledge(args.DeliveryTag, processed);
            }
        }
        private void SendSuccessfulState()
        {
            Console.WriteLine("Pong "+ DateTime.Now.ToString());

            _messageProducerScope.MessageProducer.SendMessageToQueue($"Pong" + DateTime.Now.ToString());
        }
        public void Dispose()
        {
            _messageConsumerScope.Dispose();
            _messageProducerScope.Dispose();
        }
    }
}
