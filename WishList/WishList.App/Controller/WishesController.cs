using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WishList.BusinessLogic.Models;
using WishList.Services.Exceptions;
using WishList.Services.Interfaces;

namespace WishList.App.Controller
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class WishesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IWishService wishService;

        public WishesController(IWishService wishService)
        {
            this.wishService = wishService;
        }

        [HttpGet]
        [ActionName("CreateWish")]
        public ActionResult CreateWishView()
        {
            return View();
        }

        [HttpPost]
        [ActionName("CreateWishPost")]
        public async Task<ActionResult> CreateWishAsync([FromForm] CreateWishDto wish)
        {
            var currentUser = (Infrastructure.Models.User)HttpContext.Items["User"];
            await wishService.AddWishAsync(currentUser, wish);
            return RedirectToAction("GetUserWishes", new { userId = currentUser.Id });
        }

        [HttpGet("all-wishes")]
        [ActionName("GetUserWishes")]
        public async Task<ActionResult> GetUserWishesAsync(int userId)
        {
            return View(await wishService.GetAllUserWishesAsync(userId));
        }

        [HttpGet("available-wishes")]
        public async Task<ActionResult> GetAllWishesAsync(int userId)
        {
            return Ok(await wishService.GetAvailableUserWishesAsync(userId));
        }

        [HttpGet("DeleteWish")]
        [ActionName("DeleteWish")]
        public async Task<ActionResult> DeleteWish(int id)
        {
            var wish = await wishService.GetWishById(id);
            var currentUser = (Infrastructure.Models.User)HttpContext.Items["User"];
            if (wish.UserId != currentUser.Id)
            {
                throw new AppException() { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "It is a wish of another user." };
            }

            await wishService.DeleteWishAsync(id);
            return RedirectToAction("GetUserWishes", new { userId = currentUser.Id });
        }
    }
}