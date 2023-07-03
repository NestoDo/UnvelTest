using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Umvel.Contracts.DTO.Customer;
using Umvel.Core.Handlers.Customer.Commands;
using Umvel.Core.Handlers.Customer.Queries;

namespace Umvel.Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterCustomerResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest model)
        {
            var command = new RegisterCustomerCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAllCustomerResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCustomer()
        {
            var query = new GetAllCustomerQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetCustomerByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var query = new GetCustomerByIdQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
