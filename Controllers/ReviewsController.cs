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
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _repository;
        public ReviewsController(IReviewRepository repository) => _repository = repository;

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var review = await _repository.GetByIdAsync(id);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Review review)
        {
            review.UserId = GetUserId();
            review.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(review);
            return Ok(review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Review review)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null || existing.UserId != GetUserId()) return NotFound();

            existing.Rating = review.Rating;
            existing.Comment = review.Comment;
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


