using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using WishList.Services.Exceptions;
using WishList.Services.Interfaces;
using WishList.Services.Models;

namespace WishList.Razor.App.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly IUserService userService;
        private readonly IConfiguration config;

        public LoginModel(IUserService userService, IConfiguration config)
        {
            this.userService = userService;
            this.config = config;
        }
        [BindProperty]
        public LoginRequest Person { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                //your logic for login process
                //If login usrename and password are correct then proceed to generate token
                var user = await userService.FindUserAsync(Person);
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var Sectoken = new JwtSecurityToken(config["Jwt:Issuer"],
                  config["Jwt:Issuer"],
                  new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) },
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                HttpContext.Session.SetString("Token", token);
                return RedirectToPage("AllUsers");
            }
            catch (AppException e)
            {
                ModelState.ClearValidationState(nameof(LoginRequest));
                ModelState.AddModelError("", e.Message);
                return Page();
            }

        }
    }
}
