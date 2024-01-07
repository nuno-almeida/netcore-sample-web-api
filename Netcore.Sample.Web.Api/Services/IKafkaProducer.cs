using System.Threading.Tasks;

namespace Netcore.Sample.Web.Api.Services
{
    public interface IKafkaProducer
    {
        public Task ProduceAsync(string topic, string message);
    }
}
