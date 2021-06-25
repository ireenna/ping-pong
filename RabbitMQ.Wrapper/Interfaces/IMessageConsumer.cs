using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Wrapper.Interfaces
{
    public interface IMessageConsumer
    {
        void Connect();

        void SetAcknowledge(ulong deliveryTag, bool processed);

        event EventHandler<BasicDeliverEventArgs> Received;
    }
}
