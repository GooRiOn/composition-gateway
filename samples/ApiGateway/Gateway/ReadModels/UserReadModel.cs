namespace ApiGateway.Gateway.ReadModels;

public class UserReadModel
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string ShippingAddress { get; set; }
}