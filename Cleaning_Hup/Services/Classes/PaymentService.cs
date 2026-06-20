using AutoMapper;
using Cleaning_Hup.Abstraction;
using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;
using Cleaning_Hup.Models;
using Cleaning_Hup.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Cleaning_Hup.Services.Classes
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PaymentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentResponse>> GetByOrderIdAsync(int orderId)
        {
            var payments = await _context.Payments.Where(p => p.OrderId == orderId).ToListAsync();
            return _mapper.Map<IEnumerable<PaymentResponse>>(payments);
        }

        public async Task<PaymentResponse> CreateAsync(PaymentRequest request)
        {
            var payment = _mapper.Map<Payment>(request);
            payment.PaymentDate = DateTime.UtcNow;
            _context.Payments.Add(payment);

            var order = await _context.Orders.FindAsync(request.OrderId);
            if (order != null)
            {
                order.PaidAmount += request.Amount;
                if (order.PaidAmount >= order.TotalAmount)
                    order.Status = "Completed";
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<PaymentResponse>(payment);
        }
    }
}
