using Microsoft.AspNetCore.Mvc;

namespace PracticalTest.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Content("PracticalTest API is running. See /swagger for docs.");
    }
} 
