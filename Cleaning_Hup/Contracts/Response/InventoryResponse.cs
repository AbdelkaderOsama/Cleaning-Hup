namespace Cleaning_Hup.Contracts.Reponse
{
    public class InventoryResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal MinQuantity { get; set; }
        public bool IsLowStock { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
