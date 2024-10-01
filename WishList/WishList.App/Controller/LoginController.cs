using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WishList.Services.Exceptions;
using WishList.Services.Interfaces;
using WishList.Services.Models;

namespace WishList.App.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService userService;

        public LoginController(IConfiguration config, IUserService userService)
        {
            _config = config;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
            //your logic for login process
            //If login usrename and password are correct then proceed to generate token

            var user = await userService.FindUserAsync(loginRequest);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) },
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }
    }
}
