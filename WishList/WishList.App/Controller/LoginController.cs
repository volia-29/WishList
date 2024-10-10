using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using WishList.Services.Exceptions;
using WishList.Services.Interfaces;
using WishList.Services.Models;

namespace WishList.App.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserService userService;
        private readonly HttpClient _client;

        public LoginController(IConfiguration config, IUserService userService, HttpClient client)
        {
            _config = config;
            _client = client;
            this.userService = userService;
        }

        [HttpGet]
        public ViewResult Index()
        {
            ViewData["test"] = "some value";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest)
        {
            try
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

                HttpContext.Session.SetString("Token", token);
                return RedirectToAction("GetUserWishes", "Wishes", new { userId = user.Id });
            }
            catch (AppException e)
            {
                ModelState.ClearValidationState(nameof(LoginRequest));
                ModelState.AddModelError("", e.Message);
                return View("Index", loginRequest);
            }
        }
    }
}
