using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Models;
using WishList.Services.Interfaces;

namespace WishList.Razor.App.Pages.Wishes
{
    public class MyWishesModel : PageModel
    {
        private readonly IWishService wishService;

        public MyWishesModel(IWishService wishService)
        {
            this.wishService = wishService;
        }

        public IEnumerable<Wish> Wishes { get; private set; }

        [BindProperty]
        public CreateWishDto NewWish { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = (User)HttpContext.Items["User"];
            Wishes = await wishService.GetAllUserWishesAsync(currentUser.Id);
            NewWish = new CreateWishDto();
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            var currentUser = (User)HttpContext.Items["User"];
            await wishService.AddWishAsync(currentUser, NewWish);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await wishService.DeleteWishAsync(id);
            return RedirectToPage();
        }
    }
}
