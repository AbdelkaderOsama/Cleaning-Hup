using Cleaning_Hup.Contracts.Request;
using FluentValidation;

namespace Cleaning_Hup.Validators
{
    public class InventoryRequestValidator : AbstractValidator<InventoryRequest>
    {

        public InventoryRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("يجب اختيار منتج صحيح");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("الكمية يجب أن تكون أكبر من صفر");

            RuleFor(x => x.MinQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("الحد الأدنى لا يمكن أن يكون سالباً");

            RuleFor(x => x.TransactionType)
                .NotEmpty().WithMessage("نوع العملية مطلوب")
                .Must(t => t == "IN" || t == "OUT").WithMessage("نوع العملية يجب أن يكون IN أو OUT");
        }
    }
}
