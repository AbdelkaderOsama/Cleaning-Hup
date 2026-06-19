namespace Cleaning_Hup.Models
{
    public class Product
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal WholesalePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Category Category { get; set; } = null!;
        public Inventory? Inventory { get; set; }
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
    

    }
}
