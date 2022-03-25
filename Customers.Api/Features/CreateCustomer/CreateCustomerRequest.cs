using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Customers.Api.Core;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customers.Api.Features.CreateCustomer
{
    public class CreateCustomerRequest : IOperation, IRequest<Result<CreateCustomerResponse>>
    {
        [JsonIgnore]
        public string CorrelationId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    
    public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Result<CreateCustomerResponse>>
    {
        private readonly IValidator<CreateCustomerRequest> _validator;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateCustomerRequestHandler> _logger;

        public CreateCustomerRequestHandler(IValidator<CreateCustomerRequest> validator, IMediator mediator, ILogger<CreateCustomerRequestHandler> logger)
        {
            _validator = validator;
            _mediator = mediator;
            _logger = logger;
        }
        
        public async Task<Result<CreateCustomerResponse>> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<CreateCustomerResponse>.Failure(ErrorCodes.InvalidRequest, validationResult);
            }
            
            var command = new CreateCustomerCommand
            {
                CorrelationId = request.CorrelationId,
                Title = request.Title,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var operation = await _mediator.Send(command, cancellationToken);
            if (!operation.Status)
            {
                return Result<CreateCustomerResponse>.Failure(operation.ErrorCode, operation.ValidationResult);
            }

            return Result<CreateCustomerResponse>.Success(new CreateCustomerResponse
            {
                CorrelationId = request.CorrelationId,
                CustomerId = operation.Data
            });
        }
    }
}