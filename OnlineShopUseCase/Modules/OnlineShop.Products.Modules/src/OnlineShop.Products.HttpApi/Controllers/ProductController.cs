using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Products.Domain.Commands.Request;
using OnlineShop.Products.Domain.Queries.Request;
using OnlineShop.Products.Domain.Queries.Response;

namespace OnlineShop.Products.HttpApi.Controllers
{
    public class ProductController : ApiBaseController
    {
        public ProductController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest requestModel)
        {
            var allObjList = await mediator.Send(requestModel).ConfigureAwait(false);
            return Ok(allObjList);
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<IActionResult> Get([FromQuery] GetByIdProductQueryRequest requestModel)
        {
            var obj = await mediator.Send(requestModel).ConfigureAwait(false);
            if (obj == null)
                return NotFound();
            return Ok(obj);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateProductCommandRequest requestModel)
        {
            var response = await mediator.Send(requestModel).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest requestModel)
        {
            var response = await mediator.Send(requestModel).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromQuery] DeleteProductCommandRequest requestModel)
        {
            var response = await mediator.Send(requestModel).ConfigureAwait(false);
            return Ok(response);
        }
    }
}