using System.Net;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Netcore.Sample.Web.Api.Configurations;

namespace Netcore.Sample.Web.Api.Services
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(IOptions<KafkaOptions> options)
        {
            var producerconfig = new ProducerConfig
            {
                BootstrapServers = options.Value.BootstrapServers,
                ClientId = Dns.GetHostName()
            };

            _producer = new ProducerBuilder<Null, string>(producerconfig).Build();
        }

        public async Task ProduceAsync(string topic, string message)
        {
            var kafkamessage = new Message<Null, string> { Value = message, };

            await _producer.ProduceAsync(topic, kafkamessage);
        }
    }
}
