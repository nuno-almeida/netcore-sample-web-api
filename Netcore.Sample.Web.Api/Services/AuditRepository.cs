using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Netcore.Sample.Web.Api.Configurations;
using Netcore.Sample.Web.Api.Models.Entities;

namespace Netcore.Sample.Web.Api.Services
{
    public class AuditRepository : IAuditRepository
    {
        private readonly IMongoCollection<Audit> _auditsCollection;

        public AuditRepository(IOptions<MongoOptions> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _auditsCollection = mongoDatabase.GetCollection<Audit>("audits");
        }

        public async Task CreateAsync(Audit audit)
        {
            await _auditsCollection.InsertOneAsync(audit);
        }

        public async Task<List<Audit>> GetAsync()
        {
            return await _auditsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Audit> GetAsync(string id)
        {
            return await _auditsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
