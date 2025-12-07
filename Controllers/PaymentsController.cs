using Ecommerce.DTOs;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(PaymentRequestDto dto)
        {
            var created = await _paymentService.ProcessPaymentAsync(dto);
            return Ok(created);
        }
    }
}
