using Microsoft.Extensions.Localization;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using VizORM.Common.Exceptions;
using VizORM.DataService.DTO;

namespace VizORM.DataService.Middlewares
{
    public class ExceptionHandler
    {
        private readonly StringBuilder _stringBuilder;

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
            _stringBuilder = new StringBuilder();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            // TODO: Add catch ArgumentNullException.
            catch (ArgumentException argumentException)
            {
                var localizableString = _stringLocalizer["ArgumentErrorMessage"].Value;
                var parameterName = argumentException.Message;
                var message = string.Format(localizableString, parameterName);

                await HandleExceptionAsync(httpContext, argumentException,
                    HttpStatusCode.BadRequest, message);
            }

            catch (SqlException sqlException)
            {
                var message = _stringLocalizer["SqlErrorMessage"].Value;
                await HandleExceptionAsync(httpContext, sqlException,
                    HttpStatusCode.InternalServerError, message);
            }

            catch (SqlCompilerNotImplementedException sqlCompilerNotImplementedException)
            {
                var localizableString = _stringLocalizer["SqlCompilerNotImplemented"].Value;
                var sqlCompilerName = sqlCompilerNotImplementedException.Message;
                var message = string.Format(localizableString, sqlCompilerName);

                await HandleExceptionAsync(httpContext, sqlCompilerNotImplementedException,
                    HttpStatusCode.InternalServerError, message);
            }

            catch (Exception exception)
            {
                var message = _stringLocalizer["UnknownError"].Value;
                await HandleExceptionAsync(httpContext, exception,
                    HttpStatusCode.InternalServerError, message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, 
            HttpStatusCode httpStatusCode, string message)
        {
            LogException(exception, message);
            await SendErrorResponseAsync(httpContext, httpStatusCode, message);
        }

        private async Task SendErrorResponseAsync(HttpContext httpContext, HttpStatusCode httpStatusCode, string message)
        {
            _stringBuilder.Clear();

            var seeMoreInLogsMessage = _stringLocalizer["SeeMoreInLogs"].Value;
            message = _stringBuilder
                .Append(message)
                .Append(seeMoreInLogsMessage)
                .ToString();

            var error = new Error
            {
                Message = message
            };

            _stringBuilder.Clear();

            var httpResponse = httpContext.Response;
            var httpStatusCodeInt = (int)httpStatusCode;

            httpResponse.ContentType = "application/json";
            httpResponse.StatusCode = httpStatusCodeInt;

            await httpResponse.WriteAsJsonAsync(error);
        }

        private void LogException(Exception exception, string message)
        {
            _stringBuilder.Clear();

            var exceptionMessage = exception.ToString();
            var logMessage = _stringBuilder
                .Append(message)
                .Append('\n')
                .Append(exceptionMessage)
                .ToString();

            _logger.LogError(logMessage);

            _stringBuilder.Clear();
        }
    }
}
