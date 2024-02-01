using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Netcore.Sample.Web.Api.Models.DTOs;
using Netcore.Sample.Web.Api.Services;

namespace Netcore.Sample.Web.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AuditsController : ControllerBase
    {
        private readonly ILogger<AuditsController> _logger;
        private readonly IAuditRepository _auditRepository;

        public AuditsController(ILogger<AuditsController> logger, IAuditRepository auditRepository)
        {
            _logger = logger;
            _auditRepository = auditRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<AuditDTO>> Get([FromQuery] int skip = 0, [FromQuery] int limit = 10)
        {
            var audits = await _auditRepository.GetAsync();

            return audits.Select(au => AuditDTO.fromEntity(au));
        }
    }
}