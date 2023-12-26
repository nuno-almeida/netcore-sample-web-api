using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Netcore.Sample.Web.Api.Models.Entities;
using Netcore.Sample.Web.Api.Services;
using Newtonsoft.Json;

namespace Netcore.Sample.Web.Api.Configurations
{
    // https://github.com/confluentinc/confluent-kafka-dotnet/blob/master/examples/Web/RequestTimeConsumer.cs
    public class AuditKafkaConsumer : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly string _topic;
        private readonly IAuditRepository _auditRepository;

        public AuditKafkaConsumer(IOptions<KafkaOptions> options, IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;

            _topic = options.Value.TopicAudit;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = options.Value.BootstrapServers,
                GroupId = options.Value.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();

            base.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _consumer.Subscribe(_topic);

                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumer = _consumer.Consume(cancellationToken);
                    var message = consumer.Message.Value;

                    var audit = JsonConvert.DeserializeObject<Audit>(message);
                    _auditRepository.CreateAsync(audit);
                }
            }, cancellationToken);
        }
    }
}
