namespace WebApplication1.Models
{

    public class Product
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsPriceIncluded { get; set; } = false;
        public decimal DiscountPrice { get; set; } = 0;
        public int Quantity { get; set; }
    }
}
