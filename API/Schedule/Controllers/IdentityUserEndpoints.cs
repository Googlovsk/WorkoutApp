using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Schedule.Models;
using Schedule.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Schedule.Controllers
{
    public class UserRegistrationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
    }
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public static class IdentityUserEndpoints
    {
        public static IEndpointRouteBuilder MapIdentityUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/singup", CreateUser);
            app.MapPost("/singin", SingIn);

            return app;
        }


        private static async Task<IResult> CreateUser(UserManager<User> userManager, [FromBody] UserRegistrationModel userRegistrationModel)
        {
            User user = new User()
            {
                UserName = userRegistrationModel.Email,
                Email = userRegistrationModel.Email,
                PhoneNumber = userRegistrationModel.Phone,
                FullName = userRegistrationModel.FullName,
            };
            var result = await userManager.CreateAsync(user, userRegistrationModel.Password);

            if (result.Succeeded)
            {
                return Results.Ok(result);
            }
            else
            {
                return Results.BadRequest(result);
            }
        }
        private static async Task<IResult> SingIn(UserManager<User> userManager, [FromBody] LoginModel loginModel, IOptions<AppSettings> appSettings)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var singKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Value.JWTSecret));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                                new Claim("UserId", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(singKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Results.Ok(new { token });
            }
            else
            {
                return Results.BadRequest(new { message = "Email or password is incorrect" });
            }
        }
    }

}
