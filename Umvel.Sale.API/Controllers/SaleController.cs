using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Umvel.Contracts.DTO.Sale;
using Umvel.Core.Handlers.Sale.Commands;
using Umvel.Core.Handlers.Sale.Queries;

namespace Umvel.Sale.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : Controller
    {
        private readonly IMediator _mediator;

        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterSaleResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterSale([FromBody] RegisterSaleRequest model)
        {
            var command = new RegisterSaleCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetSaleResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSaleByRange([FromQuery]GetSaleByRangeRequest model)
        {
            var query = new GetSaleQuery(model.StartDate, model.EndDate);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
