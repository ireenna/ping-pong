using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Wrapper.Interfaces
{
    public interface IMessageProducer
    {
        void SendMessageToQueue(string message, string type = null);
    }
}
