using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogItemRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogItemRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            return await _dbContext.CatalogTypes
                .OrderBy(c => c.Type)
                .ToListAsync();
        }

        public async Task<int?> Add(string type)
        {
            var item = await _dbContext.AddAsync(new CatalogType
            {
                 Type = type,
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task Delete(int id)
        {
            var type = await _dbContext.CatalogTypes.FirstOrDefaultAsync(i => i.Id == id);

            if (type != null)
            {
                _dbContext.CatalogTypes.Remove(type);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Type ID={id} not found");
            }
        }

        public async Task Update(int id, string typeName)
        {
            var type = await _dbContext.CatalogTypes.FirstOrDefaultAsync(i => i.Id == id);

            if (type != null)
            {
                type.Type = typeName;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Type ID={id} not found");
            }
        }
    }
}
