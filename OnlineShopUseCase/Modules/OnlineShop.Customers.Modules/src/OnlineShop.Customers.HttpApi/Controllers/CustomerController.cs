using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Customers.Domain.Commands.Request;
using OnlineShop.Customers.Domain.Queries.Request;
using OnlineShop.Customers.Domain.Queries.Response;

namespace OnlineShop.Customers.HttpApi.Controllers
{
    public class CustomerController : ApiBaseController
    {
        public CustomerController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        public async Task<IActionResult> GetAll([FromQuery] GetAllCustomerQueryRequest requestModel)
        {
            var allObjList = await mediator.Send(requestModel);
            return Ok(allObjList);
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] GetByIdCustomerQueryRequest requestModel)
        {
            var obj = await mediator.Send(requestModel);
            if (obj == null)
                return NotFound();
            return Ok(obj);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommandRequest requestModel)
        {
            var response = await mediator.Send(requestModel);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommandRequest requestModel)
        {
            var response = await mediator.Send(requestModel);
            return Ok(response);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromQuery] DeleteCustomerCommandRequest requestModel)
        {
            var response = await mediator.Send(requestModel);
            return Ok(response);
        }
    }
}