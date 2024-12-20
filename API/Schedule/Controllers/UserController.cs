using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schedule.Data;
using Schedule.Data.Enc;
using Schedule.Models.Domain;
using System.Security.Claims;

namespace Schedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        //[HttpPost("login")]
        //public async Task<IActionResult> Login(string login, string password)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Login == login);
        //    if (user != null)
        //    {
        //        var hashedPassword = Enc.HashPassword(password, user.PasswordSalt);

        //        if (user.PasswordHash == hashedPassword)
        //        {
        //            var claims = new List<Claim>
        //            {
        //                new Claim(ClaimTypes.Name, user.Login),
        //                new Claim(ClaimTypes.Email, user.Email),
        //                new Claim(ClaimTypes.Role, user.RoleId.ToString())
        //            };

        //            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            var principal = new ClaimsPrincipal(identity);

        //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //            return Ok(new { success = true, message = "Login successful" });
        //        }
        //    }

        //    return Unauthorized(new { success = false, message = "Invalid login or password" });
        //}
        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return Ok("Logged out");
        //}
        //[HttpPost("register")]
        //public async Task<IActionResult> Register()
        //{
        //    string password = "12345678";
        //    string salt = Enc.GenerateSalt();
        //    string encPass = Enc.HashPassword(password, salt);

        //    User user = new User
        //    {
        //        Login = "testUser",
        //        FullName = "TestUser",
        //        Email = "-",
        //        Phone = "_",
        //        RoleId = 3,
        //        PasswordHash = encPass,
        //        PasswordSalt = salt
        //    };
        //    await _context.Users.AddAsync(user);
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}

        //[HttpGet("users")]
        //public async Task<IActionResult> GetUsers()
        //{
        //    var users = await _context.Users.ToListAsync();
        //    return Ok(users);
        //}
    }
}
