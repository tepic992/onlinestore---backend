using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Api.Repositories;
using System.Security.Claims;

namespace OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        public OrdersController(IOrderRepository repository) => _repository = repository;

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var orders = await _repository.GetByUserAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null || order.UserId != GetUserId()) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            order.UserId = GetUserId();
            await _repository.AddAsync(order);
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order order)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null || existing.UserId != GetUserId()) return NotFound();

            existing.Status = order.Status;
            existing.ShippingAddressId = order.ShippingAddressId;
            await _repository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null || existing.UserId != GetUserId()) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}

