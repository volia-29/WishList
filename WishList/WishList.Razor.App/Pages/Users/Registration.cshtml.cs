using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WishList.BusinessLogic.Models;
using WishList.Services.Interfaces;

namespace WishList.Razor.App.Pages.Users
{
    public class RegistrationModel : PageModel
    {
        private readonly IUserService userService;

        public RegistrationModel(IUserService userService)
        {
            this.userService = userService;
        }
        [BindProperty]
        public CreateUserDto Person { get; set; }

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

            if (Person == null)
                return NotFound();
            await userService.AddAsync(Person);

            return RedirectToPage("Login");
        }
    }
}
