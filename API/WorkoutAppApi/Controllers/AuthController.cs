using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WorkoutAppApi.Models.Domain;
using WorkoutAppApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkoutAppApi.Models.DTOs;
using WorkoutAppApi.Data;

namespace WorkoutAppApi.Controllers
{

    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<AppSettings> _appSettings;

        public AuthController(UserManager<AppUser> userManager, IOptions<AppSettings> appSettings, AppDbContext context)
        {
            _userManager = userManager;
            _appSettings = appSettings;
            _context = context;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] RegistrationModel userRegistrationModel)
        {
            var user = new AppUser
            {
                FullName = userRegistrationModel.FullName,
                UserName = userRegistrationModel.Email,
                Email = userRegistrationModel.Email,
                PhoneNumber = userRegistrationModel.Phone,
                Gender = userRegistrationModel.Gender,
            };

            var result = await _userManager.CreateAsync(user, userRegistrationModel.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!string.IsNullOrEmpty(userRegistrationModel.Role))
            {
                //return BadRequest($"Received Role: {userRegistrationModel.Role}");
                if (userRegistrationModel.Role.Equals("Teacher", StringComparison.OrdinalIgnoreCase))
                {
                    await _userManager.AddToRoleAsync(user, userRegistrationModel.Role);
                    var schedule = new TeacherSchedule
                    {
                        TeacherId = user.Id
                    };

                    _context.Schedules.Add(schedule);
                    await _context.SaveChangesAsync();
                }
                if (userRegistrationModel.Role.Equals("Student", StringComparison.OrdinalIgnoreCase))
                {
                    await _userManager.AddToRoleAsync(user, userRegistrationModel.Role);
                }
            }
            else
            {
                return BadRequest("Role is null or empty");
            }
            return Ok(new { Succeeded = result.Succeeded });
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.JWTSecret));

                var claims = new ClaimsIdentity(new Claim[]
                {
                new Claim("userID", user.Id.ToString()),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty)
                });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(securityToken) });
            }

            return BadRequest(new { message = "Email or password is incorrect" });
        }

        [HttpGet("userprofile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            // Получение ID пользователя из Claims
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "userID");
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User ID not found in token claims." });
            }

            var userId = userIdClaim.Value;
            var userDetails = await _userManager.FindByIdAsync(userId);

            if (userDetails == null)
            {
                return NotFound(new { message = "Пользователь не найден" });
            }

            return Ok(new
            {
                Email = userDetails.Email,
                FullName = userDetails.FullName,
            });
        }
    }
}
