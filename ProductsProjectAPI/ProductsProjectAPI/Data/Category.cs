namespace ProductsProjectAPI.Data
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string CategoryCode { get; set; }
        public int IsActive { get; set; }
    }
}
