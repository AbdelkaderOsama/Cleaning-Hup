using AutoMapper;
using Cleaning_Hup.Abstraction;
using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;
using Cleaning_Hup.Models;
using Cleaning_Hup.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Cleaning_Hup.Services.Classes
{
    public class OrderService : IOrderService
    {

        private readonly AppDbContext _context;
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext context, IInventoryService inventoryService, IMapper mapper)
        {
            _context = context;
            _inventoryService = inventoryService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<OrderResponse?> GetByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return null;
            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<OrderResponse> CreateAsync(OrderRequest request)
        {
            var order = new Order
            {
                CustomerId = request.CustomerId,
                Status = "Pending",
                OrderItems = request.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            order.TotalAmount = order.OrderItems.Sum(i => i.Quantity * i.UnitPrice);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // تقليل المخزون لكل منتج في الطلب
            foreach (var item in request.Items)
            {
                await _inventoryService.UpdateQuantityAsync(item.ProductId, item.Quantity, "OUT");
            }

            return (await GetByIdAsync(order.Id))!;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;
            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
