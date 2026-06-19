namespace Cleaning_Hup.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateAt { get; set; }

        public ICollection<Product> products { get; set; } = new List<Product>();

    }
}
