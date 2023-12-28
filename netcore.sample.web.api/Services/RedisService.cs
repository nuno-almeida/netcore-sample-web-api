using System;
using Microsoft.Extensions.Options;
using Netcore.Sample.Web.Api.Configurations;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Netcore.Sample.Web.Api.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _database;

        public RedisService(IOptions<RedisOptions> options)
        {
            var cluster = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
            _database = cluster.GetDatabase();
        }

        public T Get<T>(string key)
        {
            var value = _database.StringGet(key);

            if (!string.IsNullOrEmpty(value))
                return JsonConvert.DeserializeObject<T>(value);

            return default(T);
        }

        public bool Set<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.UtcNow);
            return _database.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
        }

        public bool Set<T>(string key, T value)
        {
            return _database.StringSet(key, JsonConvert.SerializeObject(value), keepTtl: true);
        }
    }
}
