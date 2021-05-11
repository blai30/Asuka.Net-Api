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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new Get.Query(id), cancellationToken);
            return response is null ? NotFound(response) : Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetAll.Query query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);
            return response is null ? NotFound(response) : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Create.Command command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> EditAsync([FromBody] Edit.Command command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _mediator.Send(new Delete.Command(id));
            return NoContent();
        }
    }
}
