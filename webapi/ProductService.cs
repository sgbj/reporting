namespace webapi;

public class ProductService
{
    private static readonly List<Product> _products = Enumerable.Range(1, 50).Select(index =>
    {
        return new Product(index, $"Product {index}", Random.Shared.Next(1, 100), Random.Shared.Next(50), Random.Shared.Next(10000));
    }).ToList();

    public IEnumerable<Product> GetProducts() => _products;
}
