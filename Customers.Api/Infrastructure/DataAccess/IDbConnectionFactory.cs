using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Customers.Api.Infrastructure.DataAccess
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
    
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly DatabaseConfig _config;

        public DbConnectionFactory(DatabaseConfig config)
        {
            _config = config;
        }
        
        public IDbConnection GetConnection()
        {
            return new SqlConnection(_config.ConnectionString);
        }
    }
}