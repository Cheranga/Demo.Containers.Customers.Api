using System.Linq;
using Customers.Api.Core;

namespace Customers.Api.Extensions
{
    public static class ResultExtensions
    {
        public static ErrorResponse ToErrorResponse<T>(this Result<T> operation)
        {
            if (operation.Status)
            {
                return null;
            }

            var errorResponse = new ErrorResponse
            {
                ErrorCode = operation.ErrorCode,
                Errors = operation.ValidationResult.Errors.Select(x => new ErrorData
                {
                    Field = x.ErrorCode,
                    Message = x.ErrorMessage
                }).ToList()
            };

            return errorResponse;
        }
    }
}