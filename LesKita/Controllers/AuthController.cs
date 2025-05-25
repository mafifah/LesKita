
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using static DevExpress.Data.Filtering.Helpers.SubExprHelper.ThreadHoppingFiltering;
using Microsoft.EntityFrameworkCore;
namespace LesKita;

[Route("auth")]
public class AuthController : Controller
{
    private readonly IDbContextFactory<LesKitaDbContext> _dbFactory;

    public AuthController(IDbContextFactory<LesKitaDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var user = await db.T2User.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            return Unauthorized("Email atau password salah.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Nama),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("IdUser", user.IdUser.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            });

        return Redirect("/");
    }

}

