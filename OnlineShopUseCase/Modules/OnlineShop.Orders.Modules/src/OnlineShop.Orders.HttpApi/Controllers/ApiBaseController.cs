using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Orders.HttpApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        internal IMediator mediator;

        public ApiBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}