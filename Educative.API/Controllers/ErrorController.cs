using Educative.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Educative.API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : DefaultController
    {
        
        public static IActionResult Error(int code)
        {
            return new ObjectResult(new HttpErrorReponse(code));
        }
    }
}