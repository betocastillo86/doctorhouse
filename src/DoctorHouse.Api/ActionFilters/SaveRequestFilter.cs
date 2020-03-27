using System.Threading.Tasks;
using DoctorHouse.Api.Models.Requests;
using DoctorHouse.Api.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DoctorHouse.Api.ActionFilters
{
    public class SaveRequestFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(context.ActionArguments.ContainsKey("model"))
            {
                var model = (SaveRequestModel)context.ActionArguments["model"];
                var validator = new RequestValidator();

                var result = validator.Validate(model);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    context.Result = new BadRequestObjectResult(result);
                    return;
                }
            }

            await next();
        }

    }
}