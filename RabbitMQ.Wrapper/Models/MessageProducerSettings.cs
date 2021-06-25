using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQ.Wrapper.Models
{
    public class MessageProducerSettings
    {
        public IModel Channel { get; set; }
        public PublicationAddress PublicationAddress { get; set; }
    }
}
