namespace Cleaning_Hup.Contracts.Request
{
    public class InventoryRequest
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal MinQuantity { get; set; }
        public string TransactionType { get; set; } = "IN"; // IN / OUT
    }
}
