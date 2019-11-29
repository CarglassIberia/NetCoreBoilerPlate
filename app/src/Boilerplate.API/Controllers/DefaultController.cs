namespace Boilerplate.API.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    public class DefaultController : ControllerBase
    {
        [HttpGet("/fail")]
        public IActionResult Fail()
        {
            throw new Exception("Error thrown on purpose");
        }

        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Swagger()
        {
            return Redirect("/swagger");
        }
    }
}