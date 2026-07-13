using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_API_Advanced.Filters
{
    public class FileLoggerExceptionFilter : IExceptionFilter
    {
        private static readonly object FileLock = new object();
        private const string LogFileName = "ApiExceptionsLog.txt";

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            var message = $"[{timestamp}] Exception Caught: {exception.Message}{Environment.NewLine}{exception.StackTrace}{Environment.NewLine}{new string('=', 60)}{Environment.NewLine}";

            // Thread-safe append to log file
            lock (FileLock)
            {
                try
                {
                    File.AppendAllText(LogFileName, message);
                }
                catch (IOException)
                {
                    // Fallback to Console if file is locked
                    Console.WriteLine("Failed to write to exception log file.");
                }
            }

            // Standardize return payload
            context.Result = new ObjectResult(new
            {
                Error = "Internal System Failure",
                Detail = "A critical system error occurred. Diagnostics have been recorded."
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
