using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Netcore.Sample.Web.Api.Utils;

namespace Netcore.Sample.Web.Api.Filters
{
    public class ModelStateValidationFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(y => y.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key, y => y.Value.Errors.First().ErrorMessage);

                context.Result = HttpResponseResult.UnprocessableEntity(description: null, errors: errors);
            }
        }
    }
}
