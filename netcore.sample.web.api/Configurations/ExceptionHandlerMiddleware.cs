using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Netcore.Sample.Web.Api.Models.DTOs;
using Newtonsoft.Json;

namespace Netcore.Sample.Web.Api.Configurations
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var responseDTO = new ExceptionResponseDTO
                {
                    Message = "Internal Server Error",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = exception.Message
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(responseDTO));
            }
        }
    }
}
