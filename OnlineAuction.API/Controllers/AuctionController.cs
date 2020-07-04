using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.API.Data;
using OnlineAuction.API.InputModels;
using OnlineAuction.API.Models;
using OnlineAuction.API.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly OnlineAuctionContext _context;

        public AuctionController(OnlineAuctionContext context)
        {
            _context = context;
        }

        // GET: api/Auction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionViewModel>>> GetAuctionModel()
        {
            return await _context.AuctionModel
                .Select(x => ModelToViewModel(x))
                .ToListAsync();
        }

        // GET: api/Auction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionViewModel>> GetAuctionModel(int id)
        {
            var auctionModel = await _context.AuctionModel.FindAsync(id);

            if (auctionModel == null)
            {
                return NotFound();
            }

            return ModelToViewModel(auctionModel);
        }

        // PUT: api/Auction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuctionModel(int id, AuctionInputModel auctionInput)
        {
            if (id != auctionInput.Id)
            {
                return BadRequest();
            }

            var auctionModel = await _context.AuctionModel.FindAsync(id);
            if (auctionModel == null)
            {
                return NotFound();
            }

            auctionModel = InputModelToModel(auctionInput, auctionModel);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AuctionModelExists(id))
            {
                return NotFound();
            }

            return Ok(ModelToViewModel(auctionModel));
        }

        // POST: api/Auction
        [HttpPost]
        public async Task<ActionResult<AuctionViewModel>> PostAuctionModel(AuctionInputModel auctionInput)
        {
            var auctionModel = new AuctionModel();
            auctionModel = InputModelToModel(auctionInput, auctionModel);

            _context.AuctionModel.Add(auctionModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuction", new { id = auctionModel.Id }, ModelToViewModel(auctionModel));
        }

        // DELETE: api/Auction/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuctionViewModel>> DeleteAuctionModel(int id)
        {
            var auctionModel = await _context.AuctionModel.FindAsync(id);
            if (auctionModel == null)
            {
                return NotFound();
            }

            _context.AuctionModel.Remove(auctionModel);
            await _context.SaveChangesAsync();

            return ModelToViewModel(auctionModel);
        }

        private bool AuctionModelExists(int id)
        {
            return _context.AuctionModel.Any(e => e.Id == id);
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
