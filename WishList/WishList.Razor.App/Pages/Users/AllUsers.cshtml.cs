using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WishList.Infrastructure.Models;
using WishList.Services.Interfaces;

namespace WishList.Razor.App.Pages.Users
{
    public class AllUsers : PageModel
    {
        private readonly IUserService userService;

        public AllUsers(IUserService userService)
        {
            this.userService = userService;
        }

        public IEnumerable<User> Users { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await userService.GetAllUsersAsync();
            return Page();
        }
    }
}
