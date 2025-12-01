using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Data;
using System.Security.Claims;

namespace OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AddressesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AddressesController(AppDbContext context) => _context = context;

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // GET: api/addresses
        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            var userId = GetUserId();
            var addresses = await _context.Addresses
                .Where(a => a.UserId == userId)
                .ToListAsync();
            return Ok(addresses);
        }

        // POST: api/addresses
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] Address address)
        {
            address.UserId = GetUserId();
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return Ok(address);
        }

        // PUT: api/addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] Address updatedAddress)
        {
            var userId = GetUserId();
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
            if (address == null) return NotFound();

            address.FullName = updatedAddress.FullName;
            address.Street = updatedAddress.Street;
            address.City = updatedAddress.City;
            address.PostalCode = updatedAddress.PostalCode;
            address.Country = updatedAddress.Country;
            address.PhoneNumber = updatedAddress.PhoneNumber;
            address.IsDefault = updatedAddress.IsDefault;


            await _context.SaveChangesAsync();
            return Ok(address);
        }

        // DELETE: api/addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var userId = GetUserId();
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
            if (address == null) return NotFound();

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Address deleted." });
        }
    }
}

