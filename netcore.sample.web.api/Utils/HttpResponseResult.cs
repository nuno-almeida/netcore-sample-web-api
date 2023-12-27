using Microsoft.AspNetCore.Mvc;
using Netcore.Sample.Web.Api.Models.DTOs;

namespace Netcore.Sample.Web.Api.Utils
{
    public static class HttpResponseResult
    {
        public static ObjectResult NotFound(string description = null)
        {
            var exceptionResponseDTO = new ExceptionResponseDTO
            {
                Message = "Not Found",
                StatusCode = System.Net.HttpStatusCode.NotFound
            };

            if (description != null)
            {
                exceptionResponseDTO.Description = description;
            }

            return new NotFoundObjectResult(exceptionResponseDTO);
        }

        public static ObjectResult BadRequest(string description = null)
        {
            var exceptionResponseDTO = new ExceptionResponseDTO
            {
                Message = "Bad Request",
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            if (description != null)
            {
                exceptionResponseDTO.Description = description;
            }

            return new BadRequestObjectResult(exceptionResponseDTO);
        }

        public static ObjectResult UnprocessableEntity(string description = null)
        {
            var exceptionResponseDTO = new ExceptionResponseDTO
            {
                Message = "Unprocessable Entity",
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity
            };

            if (description != null)
            {
                exceptionResponseDTO.Description = description;
            }

            return new UnprocessableEntityObjectResult(exceptionResponseDTO);
        }
    }
}
