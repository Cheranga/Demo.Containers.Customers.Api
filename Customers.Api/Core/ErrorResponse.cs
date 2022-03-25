using System.Collections.Generic;

namespace Customers.Api.Core
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public List<ErrorData> Errors { get; set; }
    }

    public class ErrorData
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}