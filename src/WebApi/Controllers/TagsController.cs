using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Features.Tags;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsukaApi.Controllers
{
    public class TagsController : ApiControllerBase
    {
        public TagsController(
            ILogger<TagsController> logger,
            IMediator mediator) :
            base(logger, mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetTags([FromQuery] GetAll.Query query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);

            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new Get.Query(id), cancellationToken);

            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] Create.Command command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            if (response is null)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetTag), new { response.Id }, response);
        }

        [HttpPut]
        public async Task<IActionResult> EditTag([FromBody] Edit.Command command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            if (response is null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var response = await _mediator.Send(new Delete.Command(id));

            if (response is null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
