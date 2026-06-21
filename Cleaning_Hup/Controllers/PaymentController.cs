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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IValidator<PaymentRequest> _validator;

        public PaymentController(IPaymentService paymentService, IValidator<PaymentRequest> validator)
        {
            _paymentService = paymentService;
            _validator = validator;
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
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var result = await _paymentService.CreateAsync(request);
            return Ok(result);
        }
    }
}
