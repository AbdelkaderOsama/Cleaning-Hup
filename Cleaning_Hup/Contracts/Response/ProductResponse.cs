namespace Cleaning_Hup.Contracts.Reponse
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal WholesalePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
