using Microsoft.Extensions.Localization;
using System.Data.SqlClient;
using System.Net;
using VizORM.DataService.DTO;

namespace VizORM.DataService.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandler> _logger;

        private readonly IStringLocalizer<ExceptionHandler> _stringLocalizer;

        public ExceptionHandler(
            RequestDelegate next,
            ILogger<ExceptionHandler> logger,
            IStringLocalizer<ExceptionHandler> stringLocalizer)
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

            catch (SqlException sqlException)
            {
                var message = _stringLocalizer["SqlErrorMessage"];
                await HandleExceptionAsync(httpContext, sqlException,
                    HttpStatusCode.InternalServerError, message.Value);
            }

            catch (ArgumentException argumentException)
            {
                var message = _stringLocalizer["ArgumentErrorMessage"];
                await HandleExceptionAsync(httpContext, argumentException,
                    HttpStatusCode.BadRequest, message.Value);
            }

            catch (NotImplementedException notImplementedException)
            {
                await HandleExceptionAsync(httpContext, notImplementedException,
                    HttpStatusCode.InternalServerError, notImplementedException.Message);
            }

            catch (Exception exception)
            {
                var message = _stringLocalizer["AnotherError"];
                await HandleExceptionAsync(httpContext, exception,
                    HttpStatusCode.InternalServerError, message.Value);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, 
            HttpStatusCode httpStatusCode, string message)
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
