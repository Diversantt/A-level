using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using AutoFixture;

namespace Catalog.UnitTests.Services
{
    public class CatalogServiceTest
    {
        private readonly ICatalogService _catalogService;
        private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
        private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;

        public CatalogServiceTest()
        {
            _catalogItemRepository = new Mock<ICatalogItemRepository>();
            _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();
            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            this._catalogService = new CatalogService(
                this._dbContextWrapper.Object,
                this._logger.Object,
                this._catalogItemRepository.Object,
                this._catalogBrandRepository.Object,
                this._catalogTypeRepository.Object,
                this._mapper.Object);
        }

        [Fact]
        public async Task GetCatalogItemsAsync_Success()
        {
            // arrange
            var testPageIndex = 0;
            var testPageSize = 4;
            var testTotalCount = 12;

            var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
            {
                Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
                TotalCount = testTotalCount,
            };

            var catalogItemSuccess = new CatalogItem()
            {
                Name = "TestName"
            };

            var catalogItemDtoSuccess = new CatalogItemDto()
            {
                Name = "TestName"
            };

            this._catalogItemRepository.Setup(s => s.GetByPageAsync(
                It.Is<int>(i => i == testPageIndex),
                It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedItemsSuccess);

            this._mapper.Setup(s => s.Map<CatalogItemDto>(
                It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

            // act
            var result = await this._catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

            // assert
            Assert.NotNull(result);         // result.Should().NotBeNull();
            Assert.NotNull(result.Data);    // result?.Data.Should().NotBeNull();
            Assert.Equal(testPageIndex, result.PageIndex);  //result?.PageIndex.Should().Be(testPageIndex);
            Assert.Equal(testPageSize, result.PageSize);     //result?.PageSize.Should().Be(testPageSize);
            Assert.Equal(testTotalCount, result.Count);     // result?.Count.Should().Be(testTotalCount);
        }

        [Fact]
        public async Task GetCatalogItemsAsync_Failed()
        {
            // arrange
            var testPageIndex = 1000;
            var testPageSize = 10000;

            this._catalogItemRepository.Setup(s => s.GetByPageAsync(
                It.Is<int>(i => i == testPageIndex),
                It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

            // act
            var result = await this._catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

            // assert
            Assert.Null(result);   // result.Should().BeNull();
        }

        [Fact]
        public async Task GetCatalogItemsByIdAsync_Success()
        {
            // arrange
            int testId = 1;
            CatalogItem testItem = new Fixture().Create<CatalogItem>();
            CatalogItemDto testItemDto = new CatalogItemDto()
            {
                Name = testItem.Name,
                AvailableStock = testItem.AvailableStock,
                Description = testItem.Description,
                Id = testItem.Id,
                Price = testItem.Price,
                PictureUrl = testItem.PictureFileName,
                CatalogBrand = new CatalogBrandDto
                 {
                   Id = testItem.CatalogBrand.Id,
                   Brand = testItem.CatalogBrand.Brand,
                 },
                CatalogType = new CatalogTypeDto
                {
                   Id = testItem.CatalogType.Id,
                   Type = testItem.CatalogType.Type,
                },
            };
            this._catalogItemRepository.Setup(s => s.GetCatalogItemByIdAsync(It.IsAny<int>())).ReturnsAsync(testItem);
            this._mapper.Setup(s => s.Map<CatalogItemDto>(
                It.Is<CatalogItem>(i => i.Equals(testItem)))).Returns(testItemDto);

            // act
            var result = await this._catalogService.GetCatalogItemsByIdAsync(testId);

            // assert
            Assert.NotNull(result);   // result.Should().BeNull();
            Assert.Equal(testItemDto, result);
        }

        [Fact]
        public async Task GetCatalogItemsByIdAsync_Failed()
        {
            // arrange
            int TestId = 1;
            CatalogItem testItem = null;
            CatalogItemDto testItemDto = null;

            this._catalogItemRepository.Setup(s => s.GetCatalogItemByIdAsync(It.IsAny<int>())).ReturnsAsync(testItem);
            this._mapper.Setup(s => s.Map<CatalogItemDto>(
                It.Is<CatalogItem>(i => i.Equals(testItem)))).Returns(testItemDto);

            // act
            var result = await _catalogService.GetCatalogItemsByIdAsync(TestId);

            // assert
            Assert.Null(result);
            Assert.Equal(testItemDto, result);
        }
    }
}
