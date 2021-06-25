using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Wrapper.Models;

namespace RabbitMQ.Wrapper.Interfaces
{
    public interface IMessageConsumerScopeFactory
    {
        IMessageConsumerScope Open(MessageScopeSettings messageScopeSettings);
        IMessageConsumerScope Connect(MessageScopeSettings messageScopeSettings);
    }
}
