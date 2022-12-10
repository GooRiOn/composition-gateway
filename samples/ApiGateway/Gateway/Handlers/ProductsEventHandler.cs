using ApiGateway.Gateway.ReadModels;
using CompositionGateway.Events;
using CompositionGateway.Handlers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace ApiGateway.Gateway.Handlers;

public sealed class ProductsEventHandler : ICompositionEventHandler<OrderReadModel>
{
    private readonly HttpClient _httpClient;

    public ProductsEventHandler(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5100");
    }

    public bool HandleWhen(CompositionRequested<OrderReadModel> @event)
        => @event.ReadModel.Status is "COMPLETED";

    public async Task HandleAsync(CompositionRequested<OrderReadModel> @event, CancellationToken cancellationToken = default)
    {
        var productIds = @event.ReadModel.Products.Select(x => new KeyValuePair<string, StringValues>("productIds", x.Id.ToString()));
        var uri = QueryHelpers.AddQueryString("/products", productIds);

        var products =  await _httpClient.GetFromJsonAsync<ProductReadModel[]>(uri, cancellationToken);

        if (products is null)
        {
            throw new InvalidOperationException();
        }
        
        @event.ReadModel.Products = products;
    }
}