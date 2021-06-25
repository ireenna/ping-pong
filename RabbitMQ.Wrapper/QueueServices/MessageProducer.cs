using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Wrapper.Interfaces;
using RabbitMQ.Wrapper.Models;

namespace RabbitMQ.Wrapper.QueueServices
{
    public class MessageProducer : IMessageProducer
    {
        private readonly MessageProducerSettings _messageProducerSettings;
        private readonly IBasicProperties _properties;

        public MessageProducer(MessageProducerSettings messageProducerSettings)
        {
            _messageProducerSettings = messageProducerSettings;

            _properties = _messageProducerSettings.Channel.CreateBasicProperties();
            _properties.Persistent = true;
        }

        public void SendMessageToQueue(string message, string type = null)
        {
            if (!string.IsNullOrEmpty(type))
            {
                _properties.Type = type;
            }

            var body = Encoding.UTF8.GetBytes(message);

            _messageProducerSettings.Channel.BasicPublish(_messageProducerSettings.PublicationAddress, _properties, body);
        }

        //public void SendTyped(Type type, string message)
        //{
        //    Send(message, type.AssemblyQualifiedName);
        //}
    }
}
