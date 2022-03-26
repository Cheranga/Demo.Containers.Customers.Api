namespace Customers.Api.Infrastructure.DataAccess
{
    public class DatabaseConfig
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConnectionString => $"Server={ServerName},1433;Initial Catalog={DatabaseName};User ID={UserName};Password={Password}";
    }
}