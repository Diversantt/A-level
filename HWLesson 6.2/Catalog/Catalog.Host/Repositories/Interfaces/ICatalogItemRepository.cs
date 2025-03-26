using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);

    Task<CatalogItem> GetCatalogItemByIdAsync(int id);

    Task<PaginatedItems<CatalogItem>> GetByBrandByPageAsync(int brandId, int pageIndex, int pageSize);
    Task<PaginatedItems<CatalogItem>> GetByBrandByTypeAsync(int typeId, int pageIndex, int pageSize);
    Task Delete(int id);
    Task Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
}