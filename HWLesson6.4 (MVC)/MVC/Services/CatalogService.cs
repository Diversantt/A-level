using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MVC.Models.Enums;
using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<CatalogService> _logger;

        public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }

        public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type)
        {
            var filters = new Dictionary<CatalogTypeFilter, int>();

            if (brand.HasValue)
            {
                filters.Add(CatalogTypeFilter.Brand, brand.Value);
            }

            if (type.HasValue)
            {
                filters.Add(CatalogTypeFilter.Type, type.Value);
            }

            var result = await _httpClient.SendAsync<Catalog, PaginatedItemsRequest<CatalogTypeFilter>>($"{_settings.Value.CatalogUrl}/items",
               HttpMethod.Post,
               new PaginatedItemsRequest<CatalogTypeFilter>()
               {
                   PageIndex = page,
                   PageSize = take,
                   Filters = filters
               });

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            await Task.Delay(300);

            var result = (await _httpClient.SendAsync<IEnumerable<CatalogBrand>, CatalogBrand>
               ($"{_settings.Value.CatalogUrl}/GetBrands", HttpMethod.Get, null)).Select(brand => new SelectListItem
               {
                   Value = brand.Id.ToString(),
                   Text = brand.Brand
               });;


            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            await Task.Delay(300);

            var result = (await _httpClient.SendAsync<IEnumerable<CatalogType>, CatalogType>
               ($"{_settings.Value.CatalogUrl}/GetTypes", HttpMethod.Get, null)).Select(brand => new SelectListItem
               {
                   Value = brand.Id.ToString(),
                   Text = brand.Type
               }); ;


            return result;
        }
    }
}
