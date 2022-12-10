namespace ApiGateway.Gateway.ReadModels;

public class OrderReadModel
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public decimal TotalPrice => Products.Sum(x => x.Price);
    public UserReadModel UserDetails { get; set; }
    public IEnumerable<ProductReadModel> Products { get; set; }
}