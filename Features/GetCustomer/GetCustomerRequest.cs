using System;
using System.Threading;
using System.Threading.Tasks;
using Customers.Api.Core;
using Customers.Api.Features.CreateCustomer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customers.Api.Features.GetCustomer
{
    public class GetCustomerRequest : IRequest<Result<GetCustomerResponse>>
    {
        public string CustomerId { get; set; }
    }
    
    public class GetCustomerRequestHandler : IRequestHandler<GetCustomerRequest, Result<GetCustomerResponse>>
    {
        private readonly ILogger<GetCustomerRequestHandler> _logger;

        public GetCustomerRequestHandler(ILogger<GetCustomerRequestHandler> logger)
        {
            _logger = logger;
        }
        
        public async Task<Result<GetCustomerResponse>> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

            return Result<GetCustomerResponse>.Success(new GetCustomerResponse
            {
                CustomerId = request.CustomerId,
                FullName = "Mr. Cheranga Hatangala"
            });
        }
    }
}