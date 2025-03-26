using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex);
    Task<CatalogItemDto> GetCatalogItemsByIdAsync(int id);
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsByBrandAsync(int brandId, int pageSize, int pageIndex);

    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsByTypeAsync(int typeId, int pageSize, int pageIndex);

    Task<IEnumerable<CatalogBrandDto>> GetBrandsAsync();
    Task<IEnumerable<CatalogTypeDto>> GetTypesAsync();
}