using Microsoft.Extensions.Localization;
using System.Data.SqlClient;
using System.Net;
using System.Text.Json;
using VizORM_Backend.DTO;

namespace VizORM_Backend.Middlewares
{
    public class DataExceptionHandler
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<DataExceptionHandler> _logger;

        private readonly IStringLocalizer<DataExceptionHandler> _stringLocalizer;

        public DataExceptionHandler(
            RequestDelegate next,
            ILogger<DataExceptionHandler> logger,
            IStringLocalizer<DataExceptionHandler> stringLocalizer)
        {
            _next = next;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch(SqlException sqlException)
            {
                var message = _stringLocalizer["SqlErrorMessage"];
                await HandleExceptionAsync(httpContext, sqlException, HttpStatusCode.InternalServerError, message.Value);
            }

            catch(ArgumentException argumentException)
            {
                var message = _stringLocalizer["ArgumentErrorMessage"];
                await HandleExceptionAsync(httpContext, argumentException, HttpStatusCode.BadRequest, message.Value);
            }

            catch(Exception exception)
            {
                var message = _stringLocalizer["AnotherError"];
                await HandleExceptionAsync(httpContext, exception, HttpStatusCode.InternalServerError, message.Value);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, HttpStatusCode httpStatusCode, string message)
        {
            var errorMessage = exception.ToString();
            _logger.LogError(errorMessage);

            var httpResponse = httpContext.Response;
            var httpStatusCodeInt = (int)httpStatusCode;

            httpResponse.ContentType = "application/json";
            httpResponse.StatusCode = httpStatusCodeInt;

            var error = new Error
            {
                Message = message
            };

            await httpResponse.WriteAsJsonAsync(error);
        }
    }
}
