using System.Net;
using System.Threading.Tasks;
using Customers.Api.Core;
using Customers.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Features.GetCustomer
{
    [Route("api/customers")]
    public class GetCustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetCustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{customerId}", Name = "GetCustomer")]
        public async Task<IActionResult> GetAsync([FromRoute] string customerId)
        {
            var request = new GetCustomerRequest
            {
                CustomerId = customerId
            };

            var operation = await _mediator.Send(request);
            var response = GetResponse(operation);

            return response;
        }

        private IActionResult GetResponse(Result<GetCustomerResponse> operation)
        {
            if (operation.Status)
            {
                return Ok(operation.Data);
            }

            var errorResponse = operation.ToErrorResponse();
            return operation.ErrorCode switch
            {
                ErrorCodes.NotFound => NotFound(errorResponse),
                _ => new ObjectResult(errorResponse) {StatusCode = (int) (HttpStatusCode.InternalServerError)}
            };
        }
    }
}