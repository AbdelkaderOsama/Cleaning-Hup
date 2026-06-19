namespace Cleaning_Hup.Contracts.Request
{
    public class PaymentRequest
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }
}
