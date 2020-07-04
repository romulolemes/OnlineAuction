using BaseAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.API.Data;
using OnlineAuction.API.InputModels;
using OnlineAuction.API.Models;
using OnlineAuction.API.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.API.Services
{
    public class AuctionService
    {
        private readonly OnlineAuctionContext _context;

        public AuctionService(OnlineAuctionContext context)
        {
            _context = context;
        }

        public async Task<List<AuctionViewModel>> GetAllAsync()
        {
            return await _context.AuctionModel
                .Select(x => ModelToViewModel(x))
                .ToListAsync();
        }

        public async Task<AuctionViewModel> GetById(int id)
        {
            AuctionModel auctionModel = await FindById(id);

            return ModelToViewModel(auctionModel);
        }

        public async Task<AuctionViewModel> CreateAsync(AuctionInputModel auctionInput)
        {
            var auctionModel = new AuctionModel();
            auctionModel = InputModelToModel(auctionInput, auctionModel);

            _context.AuctionModel.Add(auctionModel);
            await _context.SaveChangesAsync();

            return ModelToViewModel(auctionModel);
        }

        public async Task<AuctionViewModel> UpdateAsync(int id, AuctionInputModel auctionInput)
        {
            var auctionModel = await FindById(id);

            auctionModel = InputModelToModel(auctionInput, auctionModel);

            await _context.SaveChangesAsync();

            return ModelToViewModel(auctionModel);
        }

        public async Task<AuctionViewModel> DeleteAsync(int id)
        {
            var auctionModel = await FindById(id);

            _context.AuctionModel.Remove(auctionModel);
            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();

            return ModelToViewModel(auctionModel);
        }

        private async Task<AuctionModel> FindById(int id)
        {
            var auctionModel = await _context.AuctionModel.FindAsync(id);

            if (auctionModel == null)
            {
                throw new ItemNotFoundException();
            }

            return auctionModel;
        }


        private static AuctionViewModel ModelToViewModel(AuctionModel auctionModel) =>
            new AuctionViewModel
            {
                Id = auctionModel.Id,
                Name = auctionModel.Name,
                InitialValue = auctionModel.InitialValue,
                IsUsed = auctionModel.IsUsed,
                User = auctionModel.User,
                InitialDate = auctionModel.InitialDate,
                EndDate = auctionModel.EndDate
            };

        private static AuctionModel InputModelToModel(AuctionInputModel auctionInput, AuctionModel auctionModel)
        {
            auctionModel.Name = auctionInput.Name;
            auctionModel.InitialValue = auctionInput.InitialValue;
            auctionModel.IsUsed = auctionInput.IsUsed;
            auctionModel.User = auctionInput.User;
            auctionModel.InitialDate = auctionInput.InitialDate;
            auctionModel.EndDate = auctionInput.EndDate;
            return auctionModel;
        }
    }
}
