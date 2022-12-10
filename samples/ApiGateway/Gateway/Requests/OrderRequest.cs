namespace ApiGateway.Gateway.Requests;

public class OrderRequest
{
    public Guid OrderId { get; set; }

    public static ValueTask<OrderRequest?> BindAsync(HttpContext context)
        => ValueTask.FromResult<OrderRequest?>(new OrderRequest
        {
            OrderId = Guid.TryParse((string?) context.Request.RouteValues["orderId"], out var orderId) 
                ? orderId : Guid.Empty
        });
}