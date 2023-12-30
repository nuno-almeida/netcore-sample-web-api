using System;

namespace Netcore.Sample.Web.Api.Services
{
    public interface IRedisService
    {
        public T Get<T>(string key);
        public bool Set<T>(string key, T value);
        public bool Set<T>(string key, T value, DateTimeOffset expirationTime);
    }
}
