using System;
using System.Threading;
using System.Threading.Tasks;
using Customers.Api.Core;
using Customers.Api.Infrastructure.DataAccess;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customers.Api.Features.GetCustomer
{
    public class GetCustomerByIdQuery : IOperation, IRequest<Result<CustomerDataModel>>
    {
        public string CustomerId { get; set; }
        public string CorrelationId { get; set; }
    }

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDataModel>>
    {
        private const string query = "select * from Customers where id=@customerId";
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ILogger<GetCustomerByIdQueryHandler> _logger;

        public GetCustomerByIdQueryHandler(IDbConnectionFactory connectionFactory, ILogger<GetCustomerByIdQueryHandler> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<Result<CustomerDataModel>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                try
                {
                    connection.Open();

                    try
                    {
                        int.TryParse(request.CustomerId, out var customerId);
                        var dataModel = await connection.QuerySingleOrDefaultAsync<CustomerDataModel>(query, new
                        {
                            CustomerId = customerId
                        });
                        return Result<CustomerDataModel>.Success(dataModel);
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception, ErrorMessages.GetCustomerError);
                        return Result<CustomerDataModel>.Failure(ErrorCodes.GetCustomerError, ErrorMessages.GetCustomerError);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}