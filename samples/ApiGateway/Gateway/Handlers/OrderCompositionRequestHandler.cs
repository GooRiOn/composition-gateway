using ApiGateway.Gateway.ReadModels;
using ApiGateway.Gateway.Requests;
using CompositionGateway.Composers;
using CompositionGateway.Handlers;

namespace ApiGateway.Gateway.Handlers;

public class OrderCompositionRequestHandler : ICompositionRequestHandler<OrderRequest, OrderReadModel>
{
    private readonly IComposer _composer;

    public OrderCompositionRequestHandler(IComposer composer)
        => _composer = composer;

    public async Task<OrderReadModel> HandleAsync(OrderRequest request, CancellationToken cancellationToken = default)
    {
        var readModel = new OrderReadModel
        {
            Id = request.OrderId,
            Products = new []
            {
                new ProductReadModel{ Id = Guid.NewGuid()},
                new ProductReadModel{ Id = Guid.NewGuid()},
                new ProductReadModel{ Id = Guid.NewGuid()},
                new ProductReadModel{ Id = Guid.NewGuid()},
                new ProductReadModel{ Id = Guid.NewGuid()},
            }
        };
        
        await _composer.ComposeAsync(readModel, cancellationToken);

        return readModel;
    }
}