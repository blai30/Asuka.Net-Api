using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly ILogger _logger;
        protected readonly IMediator _mediator;

        protected ApiControllerBase(ILogger logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
