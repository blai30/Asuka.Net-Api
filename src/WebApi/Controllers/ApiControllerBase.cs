using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase<T> : ControllerBase where T : ApiControllerBase<T>
    {
        protected readonly ILogger<T> _logger;
        protected readonly IMediator _mediator;

        protected ApiControllerBase(ILogger<T> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
