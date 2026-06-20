using Cleaning_Hup.Abstraction;
using Cleaning_Hup.Contracts.Request;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cleaning_Hup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IValidator<InventoryRequest> _validator;

        public InventoryController(IInventoryService inventoryService, IValidator<InventoryRequest> validator)
        {
            _inventoryService = inventoryService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _inventoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var result = await _inventoryService.GetByProductIdAsync(productId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateQuantity(InventoryRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            await _inventoryService.UpdateQuantityAsync(request.ProductId, request.Quantity, request.TransactionType);
            return Ok();
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock()
        {
            var result = await _inventoryService.GetLowStockAsync();
            return Ok(result);
        }
    }
}

