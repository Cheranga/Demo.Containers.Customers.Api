using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Customers.Api.Core;
using Customers.Api.Features.CreateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Customers.Api.Features.GetCustomer
{
    public class GetCustomerRequest : IOperation, IRequest<Result<GetCustomerResponse>>
    {
        [JsonIgnore]
        public string CorrelationId { get; set; }
        
        [FromRoute]
        public string CustomerId { get; set; }
    }
    
    public class GetCustomerRequestHandler : IRequestHandler<GetCustomerRequest, Result<GetCustomerResponse>>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GetCustomerRequestHandler> _logger;

        public GetCustomerRequestHandler(IMediator mediator, ILogger<GetCustomerRequestHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        public async Task<Result<GetCustomerResponse>> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var query = new GetCustomerByIdQuery
            {
                CorrelationId = request.CorrelationId,
                CustomerId = request.CustomerId
            };

            var operation = await _mediator.Send(query, cancellationToken);
            if (!operation.Status)
            {
                return Result<GetCustomerResponse>.Failure(operation.ErrorCode, operation.ValidationResult);
            }

            var customer = operation.Data;
            if (customer == null)
            {
                return Result<GetCustomerResponse>.Failure(ErrorCodes.CustomerNotFound, ErrorMessages.CustomerNotFound);
            }

            return Result<GetCustomerResponse>.Success(new GetCustomerResponse
            {
                CustomerId = customer.Id,
                FullName = $"{customer.Title} {customer.FirstName} {customer.LastName}"
            });
        }
    }
}