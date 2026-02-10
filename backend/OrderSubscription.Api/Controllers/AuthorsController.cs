using Microsoft.AspNetCore.Mvc;
using OrderSubscription.Api.Responses;

namespace OrderSubscription.Api.AuthorController;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    /// <summary>
    /// Author order
    /// </summary>
    [HttpGet("detail")]
    public IActionResult Author()
    {
       return Ok(new 
        {
            name = "Dat",
            age = 26
        });
    }
}