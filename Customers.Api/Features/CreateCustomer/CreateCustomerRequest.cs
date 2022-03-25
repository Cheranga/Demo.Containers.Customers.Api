using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Customers.Api.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customers.Api.Features.CreateCustomer
{
    public class CreateCustomerRequest : IRequest<Result<CreateCustomerResponse>>
    {
        public string CorrelationId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    
    public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Result<CreateCustomerResponse>>
    {
        private readonly ILogger<CreateCustomerRequestHandler> _logger;

        public CreateCustomerRequestHandler(ILogger<CreateCustomerRequestHandler> logger)
        {
            _logger = logger;
        }
        
        public async Task<Result<CreateCustomerResponse>> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

            return Result<CreateCustomerResponse>.Success(new CreateCustomerResponse
            {
                CorrelationId = Guid.NewGuid().ToString("N"),
                CustomerId = "1"
            });
        }
    }
}