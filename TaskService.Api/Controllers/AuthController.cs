using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace TaskService.Api.Controllers;

[ApiController]
[Route("Auth")]
public class AuthController : ControllerBase
{
    [HttpGet("/token")]
    public IActionResult ReadToken()
    {
        var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
        // Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyS...

        if (string.IsNullOrEmpty(authHeader))
        {
            return BadRequest("You shall nooooot pass");
        }

        var token = authHeader.Replace("Bearer ", "");
        // Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyS... -> eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyS...
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        return Ok(new {
            Header = jwt.Header,

            Claims = jwt.Claims.Select(c => new
            {
                c.Type,
                // Type = role, name, userID
                c.Value
                // Value = Admin, Sondre, khv6c7x7asdj
            })
        });
    }
}