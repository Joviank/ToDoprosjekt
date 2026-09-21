using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TaskService.Api.Data;
using TaskService.Api.DTO;
using TaskService.Api.Security;
using TaskService.Jwt;

namespace TaskService.Api.Controllers;

[ApiController]
[Route("Auth")]
public class AuthController : ControllerBase
{
    private readonly TaskDbContext _context;
    private readonly JwtService _jwtService;
    private readonly IConfiguration _configuration;

    public AuthController(
        TaskDbContext context,
        JwtService jwtService,
        IConfiguration configuration
    )
    {
        _context = context;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingUser = _context.Users
            .FirstOrDefault(u => u.Username == request.Username);

        if (existingUser != null)
        {
            return BadRequest("Username is already taken.");
        }

        var user = new User
        {
            Username = request.Username,
            Password = HashPasswords.HashAndSaltPassword(request.Password)
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok("New user has been registered.");
    }
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Username == request.Username);
        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        var passwordIsCorrect = HashPasswords.VerifyPassword(request.Password, user.Password);
        if (!passwordIsCorrect)
        {
            return Unauthorized("Invalid username or password");
        }

        var key = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(key))
        {
            return StatusCode(500, "JWT key is missing.");
        }

        var token = _jwtService.CreateToken(user, key);
        return Ok(new
        {
            token
        });
    }

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