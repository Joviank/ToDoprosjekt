using TaskService.Jwt;

namespace TaskService.Api.Helpers;

public class UserContext
{
    private readonly JwtService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(
        JwtService jwtService,
        IHttpContextAccessor httpContextAccessor)
    {
        _jwtService = jwtService;
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserId()
    {
        var authHeader = _httpContextAccessor.HttpContext?
            .Request.Headers["Authorization"]
            .ToString();

        if (string.IsNullOrEmpty(authHeader))
        {
            return null;
        }
        var token = authHeader.Replace("Bearer ", "");

        return _jwtService.GetUserId(token);
    }
}