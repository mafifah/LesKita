// File: Controllers/TestController.cs
using Microsoft.AspNetCore.Mvc;

namespace LesKita.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("ping")]
    public IActionResult Ping() => Ok("Pong");
}
