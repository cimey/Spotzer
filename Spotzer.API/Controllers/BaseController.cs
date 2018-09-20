using Spotzer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Spotzer.API.Controllers
{
    public abstract class BaseController : ApiController
    {

        protected internal virtual IHttpActionResult GetErrorResult(Exception ex)
        {
            if (ex is CustomException)
                switch ((ex as CustomException).CustomExceptionType)
                {
                    case CustomExceptionTypeEnum.BadRequest:
                        return BadRequest((ex as CustomException).CustomMessage);
                    case CustomExceptionTypeEnum.NotFound:
                        return NotFound();
                    case CustomExceptionTypeEnum.Forbidden:
                        return StatusCode(HttpStatusCode.Forbidden);

                    default:

                        return InternalServerError((ex as CustomException));
                }
            return InternalServerError();
        }
    }
}