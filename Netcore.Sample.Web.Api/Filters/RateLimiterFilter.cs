using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Netcore.Sample.Web.Api.Services;
using Netcore.Sample.Web.Api.Utils;

namespace Netcore.Sample.Web.Api.Filters
{
    public class RateLimiterFilter : Attribute, IResourceFilter
    {
        public int MaxRequests { set; get; }
        public int DurationInSeconds { set; get; }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var redisService = context.HttpContext.RequestServices.GetService<IRedisService>();
            var path = context.HttpContext.Request.Path.Value;

            var requestedCount = redisService.Get<int>(path);

            if (requestedCount >= MaxRequests)
            {
                context.HttpContext.Response.Headers.Add("Retry-After", DurationInSeconds.ToString());
                context.Result = HttpResponseResult.TooManyRequests();
            }
            else
            {
                if (requestedCount == 0)
                    redisService.Set<int>(path, 1, DateTimeOffset.UtcNow.AddSeconds(DurationInSeconds));
                else
                    redisService.Set<int>(path, requestedCount + 1);
            }
        }
    }
}
