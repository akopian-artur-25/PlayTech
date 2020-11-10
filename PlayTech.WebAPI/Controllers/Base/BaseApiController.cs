using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PlayTech.WebAPI.Controllers.Base
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : Controller
    {
        protected const string ErrorKey = "errors";
        protected IActionResult Result(HttpStatusCode statusCode, object content = null)
        {
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(ModelState);

                case HttpStatusCode.Forbidden:
                    return new ForbidResult();

                case HttpStatusCode.NotFound:
                    return new NotFoundResult();

                case HttpStatusCode.InternalServerError:
                    return StatusCode((int)HttpStatusCode.InternalServerError);

                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedResult();

                default:
                    return content != null
                        ? new OkObjectResult(content)
                        : (IActionResult)new OkResult();
            }
        }
    }
}
