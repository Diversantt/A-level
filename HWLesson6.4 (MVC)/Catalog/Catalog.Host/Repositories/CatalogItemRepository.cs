using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter)
    {
        IQueryable<CatalogItem> query = _dbContext.CatalogItems;

        if (brandFilter.HasValue)
        {
            query = query.Where(w => w.CatalogBrandId == brandFilter.Value);
        }

        if (typeFilter.HasValue)
        {
            query = query.Where(w => w.CatalogTypeId == typeFilter.Value);
        }

        var totalItems = await query.LongCountAsync();

        var itemsOnPage = await query.OrderBy(c => c.Name)
           .Include(i => i.CatalogBrand)
           .Include(i => i.CatalogType)
           .Skip(pageSize * pageIndex)
           .Take(pageSize)
           .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(i => i.Id == id);
        if (item == null)
        {
            throw new Exception("Item not found");
        }

        item.Name = name;
        item.Description = description;
        item.AvailableStock = availableStock;
        item.CatalogBrandId = catalogBrandId;
        item.PictureFileName = pictureFileName;
        item.Price = price;
        item.CatalogTypeId = catalogTypeId;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CatalogItem> GetCatalogItemByIdAsync(int id)
    {
        var item = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null)
        {
            throw new InvalidOperationException($"Item with ID={id} not found");
        }

        return item;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByBrandByPageAsync(int brandId, int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems.Where(i => i.CatalogBrand.Id == brandId)
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(i => i.CatalogBrand.Id == brandId)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<PaginatedItems<CatalogItem>> GetByBrandByTypeAsync(int typeId, int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems.Where(i => i.CatalogTypeId == typeId)
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(i => i.CatalogType.Id == typeId)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task Delete(int id)
    {
        var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(i => i.Id == id);

        if (item != null)
        {
            _dbContext.CatalogItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"Item ID={id} not found");
        }
    }
}