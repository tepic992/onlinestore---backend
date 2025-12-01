using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountsController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _repository.GetAllAsync();
            return Ok(discounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _repository.GetByIdAsync(id);
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Discount discount)
        {
            await _repository.AddAsync(discount);
            return Ok(discount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Discount discount)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Code = discount.Code;
            existing.Percentage = discount.Percentage;
            existing.StartDate = discount.StartDate;
            existing.EndDate = discount.EndDate;
            existing.IsActive = discount.IsActive;
            existing.Products = discount.Products;

            await _repository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}


