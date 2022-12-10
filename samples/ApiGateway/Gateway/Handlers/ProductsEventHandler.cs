using ApiGateway.Gateway.ReadModels;
using CompositionGateway.Events;
using CompositionGateway.Handlers;

namespace ApiGateway.Gateway.Handlers;

public sealed class ProductsEventHandler : ICompositionEventHandler<OrderReadModel>
{
    public Task HandleAsync(CompositionRequested<OrderReadModel> @event, CancellationToken cancellationToken = default)
    {
        foreach (var product in @event.ReadModel.Products)
        {
            product.Name = $"Product {new Random().Next(10)}";
            product.Price = 100;
        }

        return Task.CompletedTask;
    }
}