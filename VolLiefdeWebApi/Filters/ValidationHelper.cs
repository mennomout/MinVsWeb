using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VolLiefdeWebApi.Filters;

// The filter pipeline for WebApis is much more complex.
// There are (at least) five other categories of pipeline filters.
// To acchieve the same filter as for the minimal api we can create an ActionFilterAttribute.
public class ValidationHelper : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var name = (string)context.ActionArguments["name"];

        if (string.Equals(name, "Olof", StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new BadRequestResult();
        }
    }
}
