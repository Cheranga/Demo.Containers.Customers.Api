namespace Customers.Api.Core
{
    public interface IOperation
    {
        public string CorrelationId { get; set; }
    }
}