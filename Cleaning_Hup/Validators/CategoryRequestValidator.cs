using Cleaning_Hup.Contracts.Request;
using FluentValidation;

namespace Cleaning_Hup.Validators
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم التصنيف مطلوب")
                .MaximumLength(100).WithMessage("اسم التصنيف لا يجب أن يتجاوز 100 حرف");
        }
    }
}
