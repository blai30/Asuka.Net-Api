using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Features.ReactionRoles;
using Microsoft.AspNetCore.Mvc;

namespace AsukaApi.Controllers
{
    public class ReactionRolesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromQuery] ulong? guildId,
            [FromQuery] ulong? channelId,
            [FromQuery] ulong? messageId,
            [FromQuery] ulong? roleId,
            [FromQuery] string? reaction,
            CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(
                new Get.Query(guildId, channelId, messageId, roleId, reaction),cancellationToken);
            return Ok(response);
        }

        [HttpPost("[action]")]
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
    }
}
