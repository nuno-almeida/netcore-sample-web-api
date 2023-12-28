using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace Netcore.Sample.Web.Api.Models.DTOs
{
    public class ExceptionResponseDTO
    {
        [JsonProperty("status")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Errors { get; set; }
    }
}