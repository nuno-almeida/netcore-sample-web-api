using System.Collections.Generic;
using System.Threading.Tasks;
using Netcore.Sample.Web.Api.Models.Entities;

namespace Netcore.Sample.Web.Api.Services
{
    public interface IAuditRepository
    {
        Task<List<Audit>> GetAsync();
        Task<Audit> GetAsync(string id);
        Task CreateAsync(Audit audit);
    }
}
