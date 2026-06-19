using Cleaning_Hup.Contracts.Request;
using FluentValidation;

namespace Cleaning_Hup.Validators
{
    public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم العميل مطلوب")
                .MaximumLength(200).WithMessage("اسم العميل لا يجب أن يتجاوز 200 حرف");

            RuleFor(x => x.Phone)
                .Matches(@"^01[0-2,5]{1}[0-9]{8}$").WithMessage("رقم الهاتف غير صحيح")
                .When(x => !string.IsNullOrEmpty(x.Phone));
        }
    }
}
