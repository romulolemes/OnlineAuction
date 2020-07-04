using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.API.Data;
using OnlineAuction.API.InputModels;
using OnlineAuction.API.Models;
using OnlineAuction.API.Services;
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
        private readonly AuctionService _service;

        public AuctionController(AuctionService service)
        {
            _service = service;
        }

        // GET: api/Auction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionViewModel>>> GetAll() 
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Auction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionViewModel>> GetById(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        // PUT: api/Auction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuctionModel(int id, AuctionInputModel auctionInput)
        {
            if (id != auctionInput.Id)
            {
                return BadRequest();
            }

            return Ok(await _service.UpdateAsync(id, auctionInput));
        }

        // POST: api/Auction
        [HttpPost]
        public async Task<ActionResult<AuctionViewModel>> PostAuctionModel(AuctionInputModel auctionInput)
        {
            var auctionViewModel = await _service.CreateAsync(auctionInput);

            return CreatedAtAction("GetById", new { id = auctionViewModel.Id }, auctionViewModel);
        }

        // DELETE: api/Auction/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuctionViewModel>> DeleteAuctionModel(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
