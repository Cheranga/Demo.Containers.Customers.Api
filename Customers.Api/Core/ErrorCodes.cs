namespace Customers.Api.Core
{
    public class ErrorCodes
    {
        public const string InvalidRequest = nameof(InvalidRequest);
        public const string NotFound = nameof(NotFound);
        public const string CustomerInsertError = nameof(CustomerInsertError);
        public const string GetCustomerError = nameof(GetCustomerError);
    }

    public class ErrorMessages
    {
        public const string InvalidRequest = "invalid request";
        public const string NotFound = "customer not found";
        public const string CustomerInsertError = "error when inserting customer";
        public const string GetCustomerError = "error when getting the customer";
    }
}