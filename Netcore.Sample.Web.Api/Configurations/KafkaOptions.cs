namespace Netcore.Sample.Web.Api.Configurations
{
    public class KafkaOptions
    {
        public const string Kafka = "Kafka";

        public string BootstrapServers { get; set; }
        public string GroupId { get; set; }
        public string TopicAudit { get; set; }
    }
}
