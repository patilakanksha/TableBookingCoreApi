using TableBooking.Models;
using Newtonsoft.Json;
using System.Net;

namespace TableBooking.Utilities
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                 await _next(httpContext);
            }
            catch (Exception ex)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var responseModel = new ResponseModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                };

                var jsonResponse = JsonConvert.SerializeObject(responseModel);
                await httpContext.Response.WriteAsync(jsonResponse);               
            }
    }
}

    /// <summary>
    /// Extension method used to add the middleware to the HTTP request pipeline.
    /// </summary>
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
