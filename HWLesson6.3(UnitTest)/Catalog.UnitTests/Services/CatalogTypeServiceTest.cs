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
    public class CatalogTypeServiceTest
    {
        private readonly ICatalogTypeService _typeService;
        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogTypeService>> _logger;
        private readonly CatalogType _testType = new Fixture().Create<CatalogType>();

        public CatalogTypeServiceTest()
        {
            this._catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            this._dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            this._logger = new Mock<ILogger<CatalogTypeService>>();
            var dbContextTransaction = new Mock<IDbContextTransaction>();
            this._dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);
            this._typeService = new CatalogTypeService(this._dbContextWrapper.Object, this._logger.Object, this._catalogTypeRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;
            this._catalogTypeRepository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _typeService.Add(_testType.Type);

            // assert
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testResult = null;
            this._catalogTypeRepository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await this._typeService.Add(this._testType.Type);

            // assert
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // arrange
            this._catalogTypeRepository.Setup(repo => repo.Update(this._testType.Id, this._testType.Type))
                .Returns(Task.CompletedTask);

            // Act
            await this._typeService.Update(this._testType.Id, this._testType.Type);

            // Assert
            this._catalogTypeRepository
                .Verify(
                    repo => repo.Update(this._testType.Id, this._testType.Type), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            this._catalogTypeRepository.Setup(repo => repo.Delete(this._testType.Id))
                .Returns(Task.CompletedTask);

            // Act
            await this._typeService.Delete(this._testType.Id);

            // Assert
            this._catalogTypeRepository
                .Verify(repo => repo.Delete(this._testType.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange
            Task testResult = null;
            this._catalogTypeRepository.Setup(repo => repo.Delete(this._testType.Id)).Returns(testResult);

            // Act
            await this._typeService.Delete(this._testType.Id);

            // Assert
            this._catalogTypeRepository
                .Verify(repo => repo.Delete(this._testType.Id), Times.Once);
        }
    }
}
