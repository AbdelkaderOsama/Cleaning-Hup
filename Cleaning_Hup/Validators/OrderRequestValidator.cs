using Cleaning_Hup.Contracts.Request;
using FluentValidation;

namespace Cleaning_Hup.Validators
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("يجب اختيار عميل صحيح");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("يجب أن يحتوي الطلب على منتج واحد على الأقل");

            RuleForEach(x => x.Items).SetValidator(new OrderItemRequestValidator());
        }
    }

    public class OrderItemRequestValidator : AbstractValidator<OrderItemRequest>
    {
        public OrderItemRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("يجب اختيار منتج صحيح");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("الكمية يجب أن تكون أكبر من صفر");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("السعر يجب أن يكون أكبر من صفر");
        }
    }
}

