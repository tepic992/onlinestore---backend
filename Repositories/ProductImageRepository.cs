using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Api.Repositories
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        Task<IEnumerable<ProductImage>> GetByProductAsync(int productId);
    }

    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ProductImage>> GetByProductAsync(int productId)
            => await _context.ProductImages.Where(pi => pi.ProductId == productId).ToListAsync();
    }

}
