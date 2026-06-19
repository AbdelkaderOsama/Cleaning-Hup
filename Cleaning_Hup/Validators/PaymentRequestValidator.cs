using Cleaning_Hup.Contracts.Request;
using FluentValidation;

namespace Cleaning_Hup.Validators
{
    public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("يجب اختيار طلب صحيح");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("المبلغ يجب أن يكون أكبر من صفر");
        }
    }
}
