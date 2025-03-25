namespace ProductsProjectAPI.Data
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryID { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string CategoryCode { get; set; }
    }
}
