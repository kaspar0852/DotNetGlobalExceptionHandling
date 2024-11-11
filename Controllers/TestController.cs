using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandlerPOC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("trigger-exception")]
    public IActionResult TriggerException()
    {
        // Deliberately throw an exception to test the custom exception handler
        throw new InvalidOperationException("This is a test exception.");
    }
}
