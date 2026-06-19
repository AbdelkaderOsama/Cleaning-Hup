namespace Cleaning_Hup.Contracts.Request
{
    public class ProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal WholesalePrice { get; set; }
        public decimal RetailPrice { get; set; }
    }
}
