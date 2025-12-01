using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Data;


namespace OnlineStore.Api.Repositories
{
    public interface ICartRepository : IRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetByUserAsync(int userId);
    }

    public class CartRepository : Repository<CartItem>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<CartItem>> GetByUserAsync(int userId)
            => await _context.CartItems.Include(c => c.Product).Where(c => c.UserId == userId).ToListAsync();
    }
}
