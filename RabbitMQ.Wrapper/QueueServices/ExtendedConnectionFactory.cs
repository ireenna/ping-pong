
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQ.Wrapper.QueueServices
{
    public class ExtendedConnectionFactory : ConnectionFactory
    {
        public ExtendedConnectionFactory()
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672");
            RequestedConnectionTimeout = 30000;
            NetworkRecoveryInterval = TimeSpan.FromSeconds(30);
            AutomaticRecoveryEnabled = true;
            TopologyRecoveryEnabled = true;
            RequestedHeartbeat = 60;
        }
    }
}
