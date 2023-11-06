
namespace ProductsLesson
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public List<PropertyValue> Properties { get; set; }
    }
}