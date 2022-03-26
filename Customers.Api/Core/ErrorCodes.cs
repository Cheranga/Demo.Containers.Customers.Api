namespace Customers.Api.Core
{
    public class ErrorCodes
    {
        public const string InvalidRequest = nameof(InvalidRequest);
        public const string CustomerInsertError = nameof(CustomerInsertError);
        public const string GetCustomerError = nameof(GetCustomerError);
        public const string CustomerNotFound = nameof(CustomerNotFound);
    }

    public class ErrorMessages
    {
        public const string InvalidRequest = "invalid request";
        public const string CustomerInsertError = "error when inserting customer";
        public const string GetCustomerError = "error when getting the customer";
        public const string CustomerNotFound = "customer not found";
    }
}