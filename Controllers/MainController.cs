using Microsoft.AspNetCore.Mvc;

namespace websocket.Controllers;

[ApiController]
public class MainController : ControllerBase
{
   

    private readonly ILogger<MainController> _logger;

    public MainController(ILogger<MainController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("test")]
    public IActionResult Get()
    {
        return Ok("Hello world");
    }
}