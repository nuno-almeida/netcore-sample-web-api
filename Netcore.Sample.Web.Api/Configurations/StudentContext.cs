using Microsoft.Extensions.Options;
using Netcore.Sample.Web.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Netcore.Sample.Web.Api.Configurations
{
    public class StudentContext : DbContext
    {
        private string ConnectionString { get; set; }

        public StudentContext(IOptions<PostgresOptions> options)
        {
            ConnectionString = options.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }

        public DbSet<Student> Students { get; set; }
    }
}
