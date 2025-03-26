using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<IEnumerable<CatalogBrand>> GetBrandsAsync();

        Task<int?> Add(string brand);
        Task Delete(int id);
        Task Update(int id, string brandName);
    }
}
