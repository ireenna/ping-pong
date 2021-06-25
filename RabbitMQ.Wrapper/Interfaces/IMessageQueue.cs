using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQ.Wrapper.Interfaces
{
    public interface IMessageQueue : IDisposable
    {
        IModel Channel { get; set; }
    }
}
