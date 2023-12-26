using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Netcore.Sample.Web.Api.Configurations;
using Netcore.Sample.Web.Api.Services;
using Newtonsoft.Json;

namespace Netcore.Sample.Web.Api.Filters
{
    public class AuditFilter : Attribute, IResultFilter
    {
        public string Operation { set; get; }
        public string EntityName { set; get; }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            var kafkaProducer = context.HttpContext.RequestServices.GetService<IKafkaProducer>();
            var options = context.HttpContext.RequestServices.GetService<IOptions<KafkaOptions>>();

            var message = JsonConvert.SerializeObject(new Models.Entities.Audit
            {
                Entity = this.EntityName,
                Operation = this.Operation,
                StatusCode = context.HttpContext.Response.StatusCode
            });

            kafkaProducer.ProduceAsync(options.Value.TopicAudit, message);
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
        }
    }
}
