using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogItemRepository> _logger;

        public CatalogBrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogItemRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
        {
            return await _dbContext.CatalogBrands
                .OrderBy(c => c.Brand)
                .ToListAsync();
        }

        public async Task<int?> Add(string brand)
        {
            var item = await _dbContext.AddAsync(new CatalogBrand
            {
                Brand = brand
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task Delete(int id)
        {
            var brand = await _dbContext.CatalogBrands.FirstOrDefaultAsync(i => i.Id == id);

            if (brand != null)
            {
                _dbContext.CatalogBrands.Remove(brand);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Brand ID={id} not found");
            }
        }

        public async Task Update(int id, string brandName)
        {
            var brand = await _dbContext.CatalogBrands.FirstOrDefaultAsync(i => i.Id == id);

            if (brand != null)
            {
                brand.Brand = brandName;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Brand ID={id} not found");
            }
        }
    }
}
