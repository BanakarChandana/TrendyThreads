namespace ThrendyThreads.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public byte[] ProductImage { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public int DesignerId { get; set; }
    }
}