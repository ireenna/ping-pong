using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQ.Wrapper.Models
{
    public class MessageConsumerSettings
    {
        public bool SequentialFetch { get; set; } = true;
        public bool AutoAcknowledge { get; set; } = false;
        public IModel Channel { get; set; }
        public string QueueName { get; set; }
    }
}
