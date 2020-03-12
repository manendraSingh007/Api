using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [ApiController]
        public class ErrorsController : ControllerBase
        {
            [Route("{code}")]
            public IActionResult Error(int code)
            { var message = new Message<ApiError>();
                message.StatusCode = code.ToString();
                HttpStatusCode parsedCode = (HttpStatusCode)code;

                ApiError error = new ApiError(code, parsedCode.ToString());

                return new ObjectResult(error);
            }
        }
    }
}