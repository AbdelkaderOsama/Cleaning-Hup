using Cleaning_Hup.Abstraction;
using Cleaning_Hup.Contracts.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cleaning_Hup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _inventoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBtId(int id)
        {
            var result = await _inventoryService.GetByProductIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateQuantity(InventoryRequest request)
        {
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

