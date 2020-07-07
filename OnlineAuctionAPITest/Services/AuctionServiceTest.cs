using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineAuction.API.Data;
using OnlineAuction.API.InputModels;
using OnlineAuction.API.Models;
using OnlineAuction.API.Services;
using OnlineAuction.API.ViewModels;
using Xunit;

namespace OnlineAuctionAPITest.Services
{
    public class AuctionServiceTest
    {
        [Fact]
        public async void CreateAsync_WithAuctionModel_InsertDatabaseAsync()
        {
            Mock<DbSet<AuctionModel>> mockSet = GetMockSetAuctionModel();
            Mock<OnlineAuctionContext> mockContext = GetMockContext(mockSet);
            var auctionService = new AuctionService(mockContext.Object);

            var auctionInputModel = new AuctionInputModel
            {
                Id = 1,
                Name = "name",
                InitialValue = new decimal(1.0),
                IsUsed = true,
                User = "user",
                InitialDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            var auctionTest = await auctionService.CreateAsync(auctionInputModel);

            Assert.NotNull(auctionTest);
            Assert.Equal(auctionInputModel.Name, auctionTest.Name);
            Assert.Equal(auctionInputModel.InitialValue, auctionTest.InitialValue);
            Assert.Equal(auctionInputModel.IsUsed, auctionTest.IsUsed);
            Assert.Equal(auctionInputModel.User, auctionTest.User);
            Assert.Equal(auctionInputModel.InitialDate, auctionTest.InitialDate);
            Assert.Equal(auctionInputModel.EndDate, auctionTest.EndDate);

            mockSet.Verify(m => m.Add(It.IsAny<AuctionModel>()), Times.Once());
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_WithCreateUpdate()
        {
            Mock<DbSet<AuctionModel>> mockSet = GetMockSetAuctionModel();
            Mock<OnlineAuctionContext> mockContext = GetMockContext(mockSet);

            AuctionService auctionService = new AuctionService(mockContext.Object);
            var id = 1;
            var auctionInputModel = new AuctionInputModel
            {
                Id = id,
                Name = "name",
                InitialValue = new decimal(1.0),
                IsUsed = true,
                User = "user",
                InitialDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            var auctionModel = new AuctionModel
            {
                Id = id,
                Name = "name",
                InitialValue = new decimal(1.0),
                IsUsed = true,
                User = "user",
                InitialDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            ValueTask<AuctionModel> mockAuctionModel = new ValueTask<AuctionModel>(auctionModel);
            mockSet.Setup(x => x.FindAsync(id)).Returns(mockAuctionModel);

            var auctionViewModelResult = await auctionService.UpdateAsync(1, auctionInputModel);

            Assert.NotNull(auctionViewModelResult);
            Assert.Equal(auctionInputModel.Name, auctionViewModelResult.Name);

            mockSet.Verify(m => m.Add(It.IsAny<AuctionModel>()), Times.Never());
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task DeleteAsync_WithAuctionModelValid()
        {
            var id = 1;
            var auctionModel = new AuctionModel
            {
                Id = id,
                Name = "name",
                InitialValue = new decimal(1.0),
                IsUsed = true,
                User = "user",
                InitialDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            var data = new List<AuctionModel> { auctionModel }.AsQueryable();

            var mockSetAuctionModel = new Mock<DbSet<AuctionModel>>();
            mockSetAuctionModel.As<IQueryable<AuctionModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSetAuctionModel.As<IQueryable<AuctionModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSetAuctionModel.As<IQueryable<AuctionModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSetAuctionModel.As<IQueryable<AuctionModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            ValueTask<AuctionModel> mockAuctionModel = new ValueTask<AuctionModel>(auctionModel);
            mockSetAuctionModel.Setup(x => x.FindAsync(id)).Returns(mockAuctionModel);

            Mock<OnlineAuctionContext> mockContext = new Mock<OnlineAuctionContext>();
            mockContext.Setup(m => m.Auction).Returns(mockSetAuctionModel.Object);
            AuctionService auctionService = new AuctionService(mockContext.Object);

            var deleteResult = await auctionService.DeleteAsync(id);

            Assert.NotNull(deleteResult);
            Assert.IsType<AuctionViewModel>(deleteResult);
            mockSetAuctionModel.Verify(x => x.Remove(It.IsAny<AuctionModel>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ThrowExceptionWhenIdIsNotFound()
        {
            var data = new List<AuctionModel>().AsQueryable();

            var mockSetReason = new Mock<DbSet<AuctionModel>>();
            mockSetReason.As<IQueryable<AuctionModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSetReason.As<IQueryable<AuctionModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSetReason.As<IQueryable<AuctionModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSetReason.As<IQueryable<AuctionModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            Mock<OnlineAuctionContext> mockContext = new Mock<OnlineAuctionContext>();
            mockContext.Setup(m => m.Auction).Returns(mockSetReason.Object);
            AuctionService auctionService = new AuctionService(mockContext.Object);

            var id = 1;
            Exception ex = await Assert.ThrowsAsync<ItemNotFoundException>(() => auctionService.DeleteAsync(id));

            Assert.NotNull(ex);
            Assert.IsType<ItemNotFoundException>(ex);
            mockSetReason.Verify(x => x.Remove(It.IsAny<AuctionModel>()), Times.Never);
        }

        [Fact]
        public async Task FindAsync_WithGetByIdAsync()
        {
            Mock<DbSet<AuctionModel>> mockSet = GetMockSetAuctionModel();
            Mock<OnlineAuctionContext> mockContext = GetMockContext(mockSet);

            AuctionService auctionService = new AuctionService(mockContext.Object);
            var id = 1;
            var auctionInputModel = new AuctionInputModel
            {
                Id = id,
                Name = "name",
                InitialValue = new decimal(1.0),
                IsUsed = true,
                User = "user",
                InitialDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            var auctionModel = new AuctionModel
            {
                Id = id,
                Name = "name",
                InitialValue = new decimal(1.0),
                IsUsed = true,
                User = "user",
                InitialDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            ValueTask<AuctionModel> mockAuctionModel = new ValueTask<AuctionModel>(auctionModel);
            mockSet.Setup(x => x.FindAsync(id)).Returns(mockAuctionModel);

            var auctionViewModelResult = await auctionService.GetByIdAsync(1);

            Assert.NotNull(auctionViewModelResult);
            Assert.Equal(auctionInputModel.Name, auctionViewModelResult.Name);

        }

        private Mock<DbSet<AuctionModel>> GetMockSetAuctionModel()
        {
            var data = new List<AuctionModel> {
                new AuctionModel
                {
                    Id = 1,
                    Name = "name",
                    InitialValue = new decimal(1.0),
                    IsUsed = true,
                    User = "User",
                    InitialDate = DateTime.Now,
                    EndDate = DateTime.Now
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<AuctionModel>>();
            mockSet.As<IQueryable<AuctionModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<AuctionModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<AuctionModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<AuctionModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

        private static Mock<OnlineAuctionContext> GetMockContext(Mock<DbSet<AuctionModel>> mockSet)
        {
            Mock<OnlineAuctionContext> mockContext = new Mock<OnlineAuctionContext>();
            mockContext.Setup(m => m.Auction).Returns(mockSet.Object);
            return mockContext;
        }

    }
}
