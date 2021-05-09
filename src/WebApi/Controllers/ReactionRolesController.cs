using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Features.ReactionRoles;
using Microsoft.AspNetCore.Mvc;

namespace AsukaApi.Controllers
{
    public class ReactionRolesController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(new Get.Query(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetAll.Query query, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Create.Command command, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await Mediator.Send(new Delete.Command(id));
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteBulk.Command command)
        {
            var response = await Mediator.Send(command);
            return NoContent();
        }
    }
}
