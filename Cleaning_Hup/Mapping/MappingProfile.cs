using System.Diagnostics.Contracts;
using AutoMapper;
using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;
using Cleaning_Hup.Models;
namespace Cleaning_Hup.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryRequest, Category>();

            // Product
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<ProductRequest, Product>();

            // Inventory
            CreateMap<Inventory, InventoryResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.IsLowStock, opt => opt.MapFrom(src => src.Quantity <= src.MinQuantity));

            // Customer
            CreateMap<Customer, CustomerResponse>();
            CreateMap<CustomerRequest, Customer>();

            // Order
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.RemainingAmount, opt => opt.MapFrom(src => src.TotalAmount - src.PaidAmount))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));

            // Payment
            CreateMap<Payment, PaymentResponse>();
            CreateMap<PaymentRequest, Payment>();
        }
    }
}
