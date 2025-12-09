using Microsoft.AspNetCore.Mvc;

namespace DeliveryNew.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "Hello from Web API!" });
        }
    }
}
