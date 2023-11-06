namespace ProductsLesson
{
    public class PropertyDefinition : EntityBase
    {
        public string Name { get; set; }
        public List<PropertyValue> Values { get; set; }
    }
}