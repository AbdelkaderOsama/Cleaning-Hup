using Cleaning_Hup.Abstraction;
using Cleaning_Hup.Contracts.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cleaning_Hup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            var result = await _paymentService.GetByOrderIdAsync(orderId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentRequest request)
        {
            var result = await _paymentService.CreateAsync(request);
            return Ok(result);
        }
    }
}
