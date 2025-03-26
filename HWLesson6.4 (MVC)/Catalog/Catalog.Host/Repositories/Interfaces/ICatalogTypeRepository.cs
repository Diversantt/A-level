using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<IEnumerable<CatalogType>> GetTypesAsync();

        Task<int?> Add(string type);
        Task Delete(int id);
        Task Update(int id, string typeName);
    }
}
