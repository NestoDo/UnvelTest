using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Umvel.Contracts.DTO.Product;
using Umvel.Core.Handlers.Product.Commands;
using Umvel.Core.Handlers.Product.Queries;

namespace Umvel.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterProductResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterProduct([FromBody] RegisterProductRequest model)
        {
            var command = new RegisterProductCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAllProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProduct()
        {
            var query = new GetAllProductQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetProductByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductById(int id)
        {
            var query = new GetProductByIdQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
