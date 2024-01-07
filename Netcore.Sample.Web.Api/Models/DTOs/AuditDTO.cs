using System.ComponentModel;
using Netcore.Sample.Web.Api.Models.Entities;
using Newtonsoft.Json;

namespace Netcore.Sample.Web.Api.Models.DTOs
{
    public class AuditDTO
    {
        [JsonProperty(PropertyName = "id")]
        [DisplayName("id")]
        public string Id { get; private set; }

        [JsonProperty(PropertyName = "operation")]
        [DisplayName("operation")]
        public string Operation { get; private set; }

        [JsonProperty(PropertyName = "entity")]
        [DisplayName("entity")]
        public string Entity { get; private set; }

        [JsonProperty(PropertyName = "timestamp")]
        [DisplayName("timestamp")]
        public long Timestamp { get; private set; }

        [JsonProperty(PropertyName = "statusCode")]
        [DisplayName("statusCode")]
        public int StatusCode { get; set; }

        public static AuditDTO fromEntity(Audit audit)
        {
            return new AuditDTO
            {
                Id = audit.Id,
                Operation = audit.Operation,
                Entity = audit.Entity,
                Timestamp = audit.Timestamp,
                StatusCode = audit.StatusCode
            };
        }
    }
}
