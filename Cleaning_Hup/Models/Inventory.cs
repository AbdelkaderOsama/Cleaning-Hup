namespace Cleaning_Hup.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; } = 0;
        public decimal MinQuantity { get; set; } = 0;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public Product Product { get; set; } = null!;
    }
}
