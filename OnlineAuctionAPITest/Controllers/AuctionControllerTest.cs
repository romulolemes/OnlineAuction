using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Xunit;
using OnlineAuction.API.Controllers;
using OnlineAuction.API.InputModels;
using OnlineAuction.API.Services;
using OnlineAuction.API.ViewModels;

namespace OnlineAuctionAPITest.Controllers
{
    public class AuctionControllerTest
    {
        private readonly Mock<AuctionService> auctionServiceMock;
        private readonly AuctionController auctionController;

        public AuctionControllerTest()
        {
            auctionServiceMock = new Mock<AuctionService>(null);
            auctionController = new AuctionController(auctionServiceMock.Object);
        }

        [Fact]
        public async Task ShouldCallGetAllAsync_WhenCallGetAllAuctionAsync()
        {
            await auctionController.GetAll();

            auctionServiceMock.Verify(auctionService => auctionService.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldCallGetByIdAsync_WhenCallGetAuctionByIdAsync()
        {
            var auctionId = 1;
            await auctionController.GetById(auctionId);

            auctionServiceMock.Verify(auctionService => auctionService.GetByIdAsync(auctionId), Times.Once);
        }

        [Fact]
        public async Task ShouldCallUpdateAsync_WhenCallPutAuctionModelWithValidRequest()
        {
            var auctionId = 1;
            AuctionInputModel auctionInput = new AuctionInputModel();
            auctionInput.Id = auctionId;
            var result = await auctionController.PutAuctionModel(auctionInput.Id, auctionInput);
            var statusCodeResult = (IStatusCodeActionResult)result;

            auctionServiceMock.Verify(auctionService => auctionService.UpdateAsync(auctionId, auctionInput), Times.Once);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequest_WhenCallPutAuctionModelWithInvalidRequest()
        {
            var auctionId = 1;
            AuctionInputModel auctionInput = new AuctionInputModel();
            auctionInput.Id = 2;
            var result = await auctionController.PutAuctionModel(auctionId, auctionInput);

            auctionServiceMock.Verify(auctionService => auctionService.UpdateAsync(auctionId, auctionInput), Times.Never);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task ShouldCallCreateAsync_WhenCallPostAuctionModel()
        {
            AuctionInputModel auctionInput = new AuctionInputModel();
            AuctionViewModel auctionViewModel = new AuctionViewModel();
            auctionServiceMock.Setup(s => s.CreateAsync(auctionInput)).Returns(Task.FromResult(auctionViewModel));

            await auctionController.PostAuctionModel(auctionInput);

            auctionServiceMock.Verify(auctionService => auctionService.CreateAsync(auctionInput), Times.Once);
        }

        [Fact]
        public async Task ShouldCallDeleteAsync_WhenCalDeleteAuctionModel()
        {
            var auctionId = 1;
            await auctionController.DeleteAuctionModel(auctionId);

            auctionServiceMock.Verify(auctionService => auctionService.DeleteAsync(auctionId), Times.Once);
        }

    }
}
