using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Application.Entities;
using AsukaApi.Infrastructure.Features.ReactionRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsukaApi.Controllers
{
    public class ReactionRoleController : ApiControllerBase
    {
        public ReactionRoleController(
            ILogger<ReactionRoleController> logger,
            IMediator mediator) :
            base(logger, mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            ReactionRole? response = await _mediator.Send(new Get.Query(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetAll.Query query, CancellationToken cancellationToken)
        {
            IEnumerable<ReactionRole>? response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Create.Command command,
            CancellationToken cancellationToken)
        {
            Unit response = await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Unit response = await _mediator.Send(new Delete.Command(id));
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteBulk.Command command)
        {
            Unit response = await _mediator.Send(command);
            return NoContent();
        }
    }
}
