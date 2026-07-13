using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_API_Advanced.Filters
{
    public class SecurityAuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;

            if (!request.Headers.TryGetValue("Authorization", out var authHeaderValues))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Error = "Unauthorized Request",
                    Message = "No Authorization header found."
                });
                return;
            }

            string token = authHeaderValues.ToString();
            if (string.IsNullOrWhiteSpace(token) || !token.StartsWith("Bearer ", System.StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Error = "Unauthorized Request",
                    Message = "Bearer scheme must be used in the Authorization token."
                });
            }
        }
    }
}
