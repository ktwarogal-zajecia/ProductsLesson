namespace ProductsLesson
{
    public class Category : EntityBase
    {
        public List<Product> Products { get; set; }
        public string Name { get; set; }
        public int MyProperty { get; set; }
    }
}