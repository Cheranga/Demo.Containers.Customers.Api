using System.Net;
using System.Threading.Tasks;
using Customers.Api.Core;
using Customers.Api.Extensions;
using Customers.Api.Features.GetCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Features.CreateCustomer
{
    [Route("api/customer")]
    public class CreateCustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CreateCustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateCustomer")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerRequest request)
        {
            var operation = await _mediator.Send(request);

            var response = GetResponse(operation);

            return response;
        }

        private IActionResult GetResponse(Result<CreateCustomerResponse> operation)
        {
            if (operation.Status)
                return CreatedAtRoute("GetCustomer", new
                {
                    customerId = operation.Data.CustomerId
                }, null);

            var errorResponse = operation.ToErrorResponse();
            return operation.ErrorCode switch
            {
                ErrorCodes.InvalidRequest => BadRequest(errorResponse),
                _ => new ObjectResult(errorResponse) {StatusCode = (int) HttpStatusCode.InternalServerError}
            };
        }
    }
}