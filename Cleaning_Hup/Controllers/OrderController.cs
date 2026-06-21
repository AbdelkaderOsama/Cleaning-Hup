using Cleaning_Hup.Abstraction;
using Cleaning_Hup.Contracts.Request;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cleaning_Hup.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<OrderRequest> _validator;

        public OrderController(IOrderService orderService, IValidator<OrderRequest> validator)
        {
            _orderService = orderService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _orderService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var result = await _orderService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var result = await _orderService.UpdateStatusAsync(id, status);
            if (!result) return NotFound();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
