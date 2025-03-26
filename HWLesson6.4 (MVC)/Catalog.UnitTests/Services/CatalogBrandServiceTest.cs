using AutoFixture;
using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.UnitTests.Services
{
    public class CatalogBrandServiceTest
    {
        private readonly ICatalogBrandService _brandService;
        private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogBrandService>> _logger;
        private readonly CatalogBrand _testBrand = new Fixture().Create<CatalogBrand>();

        public CatalogBrandServiceTest()
        {
            this._catalogBrandRepository = new Mock<ICatalogBrandRepository>();
            this._dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            this._logger = new Mock<ILogger<CatalogBrandService>>();
            var dbContextTransaction = new Mock<IDbContextTransaction>();
            this._dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);
            this._brandService = new CatalogBrandService(this._dbContextWrapper.Object, this._logger.Object, this._catalogBrandRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            this._catalogBrandRepository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await this._brandService.Add(this._testBrand.Brand);

            // assert
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testResult = null;
            this._catalogBrandRepository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await this._brandService.Add(this._testBrand.Brand);

            // assert
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // arrange
            this._catalogBrandRepository.Setup(repo => repo.Update(this._testBrand.Id, this._testBrand.Brand))
                .Returns(Task.CompletedTask);

            // Act
            await this._brandService.Update(this._testBrand.Id, this._testBrand.Brand);

            // Assert
            this._catalogBrandRepository.Verify(repo => repo.Update(this._testBrand.Id, this._testBrand.Brand), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            this._catalogBrandRepository.Setup(repo => repo.Delete(this._testBrand.Id))
                .Returns(Task.CompletedTask);

            // Act
            await this._brandService.Delete(this._testBrand.Id);

            // Assert
            this._catalogBrandRepository.Verify(repo => repo.Delete(this._testBrand.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange
            Task testResult = null;
            this._catalogBrandRepository.Setup(repo => repo.Delete(this._testBrand.Id)).Returns(testResult);

            // Act
            await this._brandService.Delete(this._testBrand.Id);

            // Assert
            this._catalogBrandRepository.Verify(repo => repo.Delete(this._testBrand.Id), Times.Once);
        }
    }
}
