using Microsoft.EntityFrameworkCore;

namespace ProductsLesson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Dla osób, którym nie działa dotnet ef:
            // Instalacja nugeta: Install-Package Microsoft.EntityFrameworkCore.Tools
            // Dodanie migracji: Add-Migration MigrationName
            // Dodanie migracji do bazy danych: Update-Database


            var context = new MyContext();

            // 1-3.: Utworzenie pierwotnych wartości
            //AddInitialValues(context);
                      

            // 4. Pobranie całej struktury z bazy

            var products = context
                .Products
                    .Include(p => p.Category)
                    .Include(p => p.Properties)
                        .ThenInclude(p => p.PropertyDefinition)
                .ToList();

            DescribeProducts(products);




            // 5. Pobieranie po konkretnej właściwości

            var rgbOnly = context
                .Products
                .Where(p => p.Properties
                    .Any(p => p.Value == "RGB"))
                .ToList();
            DescribeProducts(rgbOnly);

            var rgbOnlyQuerySyntax = (from product in context.Products
                                      where product.Properties.Any(p => p.Value == "RGB")
                                      select product).ToList();


            DescribeProducts(rgbOnlyQuerySyntax);


        }

        private static void AddInitialValues(MyContext context)
        {
            // 1. Dodanie właściwości
            var propDefColor = new PropertyDefinition
            {
                Name = "Kolor",
            };

            var propDefCountry = new PropertyDefinition
            {
                Name = "Kraj",
            };

            context.PropertyDefinitions.Add(propDefColor);
            context.PropertyDefinitions.Add(propDefCountry);

            context.SaveChanges();




            // 2. Dodanie kategorii

            var categoryProcessor = new Category { Name = "Procesory" };
            var categoryKeyboard = new Category { Name = "Klawiatury" };

            context.Categories.Add(categoryProcessor);
            context.Categories.Add(categoryKeyboard);

            context.SaveChanges();



            // 3. Dodanie produktu z kategorią i właściwościami

            var produkt1 = new Product
            {
                Name = "Klawiatura Logitech",
                Category = categoryKeyboard,
                Properties = new List<PropertyValue>
                {
                    new PropertyValue
                    {
                        PropertyDefinition = propDefColor,
                        Value = "RGB",
                    },
                    new PropertyValue
                    {
                        PropertyDefinition = propDefCountry,
                        Value = "USA",
                    },
                },
            };

            var produkt2 = new Product
            {
                Name = "Procesor Intel",
                Category = categoryProcessor,
                Properties = new List<PropertyValue>
                {
                    new PropertyValue
                    {
                        PropertyDefinition = propDefCountry,
                        Value = "USA",
                    },
                },
            };

            context.Products.Add(produkt1);
            context.Products.Add(produkt2);
            context.SaveChanges();
        }

        private static void DescribeProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Nazwa: {product.Name}, cena: {product.Price}");
                Console.WriteLine($"Kategoria: {product.Category.Name}");
                Console.WriteLine("Wlasciwosci:");
                foreach (var prop in product.Properties)
                {
                    Console.WriteLine($"- {prop.PropertyDefinition.Name} : {prop.Value}");
                }
            }
        }
    }
}