using Cleaning_Hup.Abstraction;
using Cleaning_Hup.Contracts.Response;
using Cleaning_Hup.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Cleaning_Hup.Services.Classes
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SalesReportResponse> GetSalesReportAsync(DateTime fromDate, DateTime toDate)
        {
            var orders = await _context.Orders
                .Where(o => o.OrderDate >= fromDate && o.OrderDate <= toDate)
                .ToListAsync();

            return new SalesReportResponse
            {
                FromDate = fromDate,
                ToDate = toDate,
                TotalOrders = orders.Count,
                TotalSales = orders.Sum(o => o.TotalAmount),
                TotalPaid = orders.Sum(o => o.PaidAmount),
                TotalRemaining = orders.Sum(o => o.TotalAmount - o.PaidAmount)
            };
        }
    }
}
