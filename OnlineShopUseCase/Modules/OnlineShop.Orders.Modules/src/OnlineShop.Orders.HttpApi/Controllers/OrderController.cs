using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Orders.Domain.Commands.Request;
using OnlineShop.Orders.Domain.Queries.Request; 
using Swashbuckle.AspNetCore.Annotations;

namespace OnlineShop.Orders.HttpApi.Controllers
{
    public class OrderController : ApiBaseController
    {
        public OrderController(IMediator mediator) : base(mediator)
        {
        }
         
        [SwaggerOperation(Summary = "Returns all orders between given dates")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        public async Task<IActionResult> GetAll([FromQuery] GetAllOrderQueryRequest requestModel)
        { 
            var allObjList = await mediator.Send(requestModel).ConfigureAwait(false);
            return Ok(allObjList);
        }

        [SwaggerOperation(Summary = "Returns the order given by the id")]
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] GetByIdOrderQueryRequest requestModel)
        {
            var obj = await mediator.Send(requestModel).ConfigureAwait(false);
            if (obj == null)
                return NotFound();
            return Ok(obj);
        }

        [SwaggerOperation(Summary = "Creates a new order")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommandRequest requestModel)
        {
            var response = await mediator.Send(requestModel).ConfigureAwait(false);
            return Ok(response);
        } 
    }
}