namespace Customers.Api.Features.CreateCustomer
{
    public class CreateCustomerCommand
    {
        public string CorrelationId { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}