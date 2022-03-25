using System;
using System.Threading;
using System.Threading.Tasks;
using Customers.Api.Core;
using Customers.Api.Infrastructure.DataAccess;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customers.Api.Features.CreateCustomer
{
    public class CreateCustomerCommand : IOperation, IRequest<Result<string>>
    {
        public string CorrelationId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    
    public class CreateCustomerCommandHandler :IRequestHandler<CreateCustomerCommand, Result<string>>
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        
        private const string InsertCommand = "insert into Customers (Title, FirstName, LastName) " +
                                             "output inserted.Id, inserted.Title, inserted.FirstName, inserted.LastName " +
                                             "values (@Title, @FirstName, @LastName)";

        public CreateCustomerCommandHandler(IDbConnectionFactory connectionFactory, ILogger<CreateCustomerCommandHandler> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        
        public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                try
                {
                    connection.Open();
                    
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var dataModel = await connection.QuerySingleOrDefaultAsync<CustomerDataModel>(InsertCommand, request, transaction);
                            transaction.Commit();

                            return Result<string>.Success(dataModel.Id);
                        }
                        catch (Exception exception)
                        {
                            _logger.LogError(exception, ErrorMessages.CustomerInsertError);
                            transaction.Rollback();
                            return Result<string>.Failure(ErrorCodes.CustomerInsertError, ErrorMessages.CustomerInsertError);
                        }
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