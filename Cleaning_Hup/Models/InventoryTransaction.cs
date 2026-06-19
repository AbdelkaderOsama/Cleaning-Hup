namespace Cleaning_Hup.Models
{
    public class InventoryTransaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string TransactionType { get; set; } = string.Empty; // IN / OUT
        public decimal Quantity { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Product Product { get; set; } = null!;
    }
}
