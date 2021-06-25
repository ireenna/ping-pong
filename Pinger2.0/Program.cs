using System;
using Ponger;
using RabbitMQ.Client;
using RabbitMQ.Wrapper.Interfaces;
using RabbitMQ.Wrapper.QueueServices;

namespace Pinger2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            IConnectionFactory connect = new ExtendedConnectionFactory();
            IMessageProducerScopeFactory producer1 = new MessageProducerScopeFactory(connect);
            IMessageConsumerScopeFactory consumer1 = new MessageConsumerScopeFactory(connect);
            QueueService qs = new QueueService(producer1, consumer1);
            MessageService ms = new MessageService(producer1, consumer1);
            MessageService ms1 = new MessageService(producer1, consumer1);
            qs.Ping("Ping "+ DateTime.Now.ToString());
        }
    }
}
