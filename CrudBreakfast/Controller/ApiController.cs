using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CrudBreakfast.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {

            if (errors.All (e => e.Type == ErrorType.Validation))
            {
                var modelState = new ModelStateDictionary();
                foreach (var error in errors)
                {
                    modelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem(modelState);
            }

            if (errors.Any(e => e.Type == ErrorType.Unexpected))
            {
                return Problem(statusCode: 500, title: "An unexpected error occurred");
            }
            var firstError = errors[0];

            var statusCode = firstError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: firstError.Description);
        }
    }
}