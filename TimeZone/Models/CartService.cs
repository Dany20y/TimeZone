using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Time_Zone.Domain.Entities.Product;
using Time_Zone.BussinessLogic.DBModel;

namespace Time_Zone.Models
{
    public class CartService
    {
        private readonly ProductContext _context;

        public CartService(ProductContext context)
        {
            _context = context;
        }

        public async Task AddToCart(int productId, string userId, int quantity)
        {
            var cartLine = await _context.CartLines
                .FirstOrDefaultAsync(cl => cl.ProductId == productId && cl.UserId == userId);

            if (cartLine == null)
            {
                cartLine = new CartLine
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = quantity,
                    Price = _context.Products.Find(productId).Price
                };
                _context.CartLines.Add(cartLine);
            }
            else
            {
                cartLine.Quantity += quantity;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<CartLine>> GetUserCart(string userId)
        {
            return await _context.CartLines
                .Include(cl => cl.Product)
                .Where(cl => cl.UserId == userId)
                .ToListAsync();
        }

        public async Task RemoveFromCart(int productId, string userId)
        {
            var cartLine = await _context.CartLines
                .FirstOrDefaultAsync(cl => cl.ProductId == productId && cl.UserId == userId);

            if (cartLine != null)
            {
                _context.CartLines.Remove(cartLine);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCart(CartLine updatedCartLine)
        {
            var cartLine = await _context.CartLines
                .FirstOrDefaultAsync(cl => cl.ProductId == updatedCartLine.ProductId && cl.UserId == updatedCartLine.UserId);

            if (cartLine != null)
            {
                cartLine.Quantity = updatedCartLine.Quantity;
                cartLine.Price = updatedCartLine.Price;
                await _context.SaveChangesAsync();
            }
        }
    }
}
