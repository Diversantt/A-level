using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Catalog.Host.Data.Entities;
using AutoFixture;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.UnitTests.Services
{
    public class CatalogItemServiceTest
    {
        private readonly ICatalogItemService _catalogService;
        private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly CatalogItem _testItem = new Fixture().Create<CatalogItem>();

        public CatalogItemServiceTest()
        {
            this._catalogItemRepository = new Mock<ICatalogItemRepository>();
            this._dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            this._logger = new Mock<ILogger<CatalogService>>();
            var dbContextTransaction = new Mock<IDbContextTransaction>();
            this._dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);
            this._catalogService = new CatalogItemService(this._dbContextWrapper.Object, this._logger.Object, this._catalogItemRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            this._catalogItemRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService
                .Add(
                this._testItem.Name,
                this._testItem.Description,
                this._testItem.Price,
                this._testItem.AvailableStock,
                this._testItem.CatalogBrandId,
                this._testItem.CatalogTypeId,
                this._testItem.PictureFileName);

            // assert
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testResult = null;

            this._catalogItemRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await this._catalogService
                .Add(
                 this._testItem.Name,
                 this._testItem.Description,
                 this._testItem.Price,
                 this._testItem.AvailableStock,
                 this._testItem.CatalogBrandId,
                 this._testItem.CatalogTypeId,
                 this._testItem.PictureFileName);

            // assert
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // arrange
            this._catalogItemRepository.Setup(repo => repo
            .Update(
                this._testItem.Id,
                this._testItem.Name,
                this._testItem.Description,
                this._testItem.Price,
                this._testItem.AvailableStock,
                this._testItem.CatalogBrandId,
                this._testItem.CatalogTypeId,
                this._testItem.PictureFileName))
                .Returns(Task.CompletedTask);

            // Act
            await this._catalogService
                .Update(
                this._testItem.Id,
                this._testItem.Name,
                this._testItem.Description,
                this._testItem.Price,
                this._testItem.AvailableStock,
                this._testItem.CatalogBrandId,
                this._testItem.CatalogTypeId,
                this._testItem.PictureFileName);

            // Assert
            this._catalogItemRepository
                .Verify(
                    repo => repo.Update(
                        this._testItem.Id,
                        this._testItem.Name,
                        this._testItem.Description,
                        this._testItem.Price,
                        this._testItem.AvailableStock,
                        this._testItem.CatalogBrandId,
                        this._testItem.CatalogTypeId,
                        this._testItem.PictureFileName), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_RowUpsent()
        {
            // arrange
            this._catalogItemRepository
                .Setup(repo => repo
                .Update(
                 this._testItem.Id,
                 this._testItem.Name,
                 this._testItem.Description,
                 this._testItem.Price,
                 this._testItem.AvailableStock,
                 this._testItem.CatalogBrandId,
                 this._testItem.CatalogTypeId,
                 this._testItem.PictureFileName))
                  .ThrowsAsync(new Exception("Item not found"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _catalogService
            .Update(
                this._testItem.Id,
                this._testItem.Name,
                this._testItem.Description,
                this._testItem.Price,
                this._testItem.AvailableStock,
                this._testItem.CatalogBrandId,
                this._testItem.CatalogTypeId,
                this._testItem.PictureFileName));

            // Assert
            Assert.Equal("Item not found" , exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            this._catalogItemRepository.Setup(repo => repo.Delete(this._testItem.Id))
                .Returns(Task.CompletedTask);

            // Act
            await this._catalogService.Delete(this._testItem.Id);

            // Assert
            this._catalogItemRepository
                .Verify(repo => repo.Delete(this._testItem.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange
            Task testResult = null;
            this._catalogItemRepository.Setup(repo => repo.Delete(this._testItem.Id)).Returns(testResult);

            // Act
            await this._catalogService.Delete(this._testItem.Id);

            // Assert
            this._catalogItemRepository
                .Verify(repo => repo.Delete(this._testItem.Id), Times.Once);
        }
    }
}
