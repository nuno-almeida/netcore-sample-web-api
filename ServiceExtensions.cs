using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netcore.Sample.Web.Api.Configurations;

namespace Netcore.Sample.Web.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PostgresOptions>(configuration.GetSection(PostgresOptions.Postgres));
            services.AddSingleton<StudentContext>();
        }
    }
}
