﻿using Microsoft.EntityFrameworkCore;
using WishList.BusinessLogic.Models;
using WishList.Infrastructure.Data;
using WishList.Infrastructure.Models;
using WishList.Services.Exceptions;
using WishList.Services.Interfaces;
using WishList.Services.Models;

namespace WishList.Services.Services
{
    public class WishService : IWishService
    {
        private readonly WishListContext _context;

        public WishService(WishListContext context)
        {
            _context = context;
        }

        public async Task AddWishAsync(User currentUser, CreateWishDto wish)
        {
            var new_wish = new Wish()
            {
                Id = await _context.Wishes.CountAsync() + 1,
                Description = wish.Description
            };
            await _context.Wishes.AddAsync(new_wish);
            currentUser.Wishes.Add(new_wish);
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteWishAsync(int id)
        {
            return await _context.Wishes.Where(w => w.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<Wish>> GetAllUserWishesAsync(int userId)
        {
            return await _context.Wishes.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<GetWishDto>> GetAvailableUserWishesAsync(int userId)
        {
            return await _context.Wishes
                .Where(w => w.UserId == userId && w.IsSelected == 0)
                .Select(w => new GetWishDto() { Description = w.Description, Id = w.Id })
                .ToListAsync();
        }

        public async Task ChooseWishAsync(User user, ChoseWishDto choseWish)
        {
            if (choseWish.Select)
                await ChooseWishAsync(user, choseWish.WishId);
            else
                await UnchooseWishAsync(choseWish.WishId);
        }

        private async Task ChooseWishAsync(User user, int wishId)
        {
            var wish = await _context.Wishes.Where(w => w.Id == wishId).FirstOrDefaultAsync();
            wish.IsSelected = 1;
            await _context.WishFulfillments.AddAsync(
                new WishFulfillment()
                {
                    Wish = wish,
                    WishGranter = user,
                }
            );
            await _context.SaveChangesAsync();
        }

        private async Task UnchooseWishAsync(int wishId)
        {
            var wish = await _context.Wishes.Where(w => w.Id == wishId).FirstOrDefaultAsync();
            wish.IsSelected = 0;
            var wishFulfillment = await _context.WishFulfillments.Where(w => w.Wish == wish).FirstOrDefaultAsync();
            _context.Remove<WishFulfillment>(wishFulfillment);
            await _context.SaveChangesAsync();
        }

        public async Task<Wish> GetWishById(int wishId)
        {
            var wish = await _context.Wishes.FirstOrDefaultAsync(w => w.Id == wishId);

            if (wish == null)
            {
                throw new NotFoundException();
            }

            return wish;
        }
    }
}
