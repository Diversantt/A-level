namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> Add(string type);
        Task Delete(int id);
        Task Update(int id, string typeName);
    }
}
