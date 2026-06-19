using Cleaning_Hup.Contracts.Request;
using FluentValidation;

namespace Cleaning_Hup.Validators
{
    public class ProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public ProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم المنتج مطلوب")
                .MaximumLength(200).WithMessage("اسم المنتج لا يجب أن يتجاوز 200 حرف");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("يجب اختيار تصنيف صحيح");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("وحدة القياس مطلوبة");

            RuleFor(x => x.WholesalePrice)
                .GreaterThan(0).WithMessage("سعر الجملة يجب أن يكون أكبر من صفر");

            RuleFor(x => x.RetailPrice)
                .GreaterThan(0).WithMessage("سعر التجزئة يجب أن يكون أكبر من صفر")
                .GreaterThanOrEqualTo(x => x.WholesalePrice).WithMessage("سعر التجزئة يجب أن يكون أكبر من أو يساوي سعر الجملة");
        }
    }
}
