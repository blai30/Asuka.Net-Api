﻿using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Features.ReactionRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsukaApi.Controllers
{
    public class ReactionRolesController : ApiControllerBase
    {
        public ReactionRolesController(
            ILogger<ReactionRolesController> logger,
            IMediator mediator) :
            base(logger, mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new Get.Query(id), cancellationToken);

            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetAll.Query query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);

            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Create.Command command,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            if (response is null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new Delete.Command(id));

            if (response is null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteBulk.Command command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
