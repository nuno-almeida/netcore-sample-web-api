using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netcore.Sample.Web.Api.Configurations;
using Netcore.Sample.Web.Api.Services;

namespace Netcore.Sample.Web.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PostgresOptions>(configuration.GetSection(PostgresOptions.Postgres));
            services.AddSingleton<StudentContext>();
        }

        public static void AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection(MongoOptions.Mongo));
            services.AddSingleton<IAuditRepository, AuditRepository>();
        }
    }
}
