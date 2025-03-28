//using System.Text.Json;

//namespace Ebisx.POS.Presentation.Services;

//public class MockDataService
//{
//    private readonly string _filePath;

//    public MockDataService()
//    {
//        _filePath = Path.Combine(FileSystem.AppDataDirectory, "products.json");
//    }

//    public async Task<ObservableCollection<Product>> LoadProductsAsync()
//    {
//        if (!File.Exists(_filePath))
//        {
//            var mockProducts = GenerateMockProducts(10);
//            await SaveProductsAsync(mockProducts);
//        }

//        string json = await File.ReadAllTextAsync(_filePath);
//        return JsonSerializer.Deserialize<ObservableCollection<Product>>(json) ?? new();
//    }

//    public async Task SaveProductsAsync(ObservableCollection<Product> products)
//    {
//        string json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
//        await File.WriteAllTextAsync(_filePath, json);
//    }

//    private ObservableCollection<Product> GenerateMockProducts(int count = 20)
//    {
//        Faker<Product>? faker = new Faker<Product>()
//            .RuleFor(p => p.Id, f => f.Random.Int(1000, 9999))
//            .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
//            .RuleFor(p => p.Barcode, f => f.Random.ReplaceNumbers("############")) // 12-digit barcode
//            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 100))
//            .RuleFor(p => p.Price, f => f.Random.Decimal(50, 5000))
//            .RuleFor(p => p.Vat, f => f.Random.Bool() ? 12m : 0m) // VAT is either 12% or 0%
//            .RuleFor(p => p.SalesUnit, f => f.PickRandom(new[] { "Piece", "Box", "Pack", "KG", "Liter" }));

//        return new ObservableCollection<Product>(faker.Generate(count));
//    }

//}
