using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Netcore.Sample.Web.Api.Models.Entities
{
    public class Audit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        [BsonElement("operation")]
        public string Operation { get; set; }

        [BsonElement("entity")]
        public string Entity { get; set; }

        [BsonElement("statusCode")]
        public int StatusCode { get; set; }

        [BsonElement("timestamp")]
        public long Timestamp { get; private set; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }
}
