
using Infrastructure.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/MaritalStatuses")]
    //[Authorize]
    public class MaritalStatusesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaritalStatusesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin")] // فقط الأدوار المحددة تستطيع عرض الصلاحيات

        // GET: api/Applicants
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string? filter = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool sortDesc = false,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            Expression<Func<MaritalStatus, bool>>? filterExpr = null;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filterExpr = a => a.Description.Contains(filter!);
            }

            var query = new GetEntitiesQuery<MaritalStatus, MaritalStatusDto>(
                filterExpr,
                null,
                sortBy,
                sortDesc,
                page,
                pageSize
                ,
                    new Expression<Func<MaritalStatus, object>>[] {  }

            );

            var result = await _mediator.Send(query);
            return Ok(ApiResult.Ok(result, "Fetched all data!", 200, HttpContext.TraceIdentifier));
        }

    }
}
