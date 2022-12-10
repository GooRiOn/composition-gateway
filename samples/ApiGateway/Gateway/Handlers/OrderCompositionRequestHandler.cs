using ApiGateway.Gateway.ReadModels;
using ApiGateway.Gateway.Requests;
using CompositionGateway.Composers;
using CompositionGateway.Handlers;

namespace ApiGateway.Gateway.Handlers;

public class OrderCompositionRequestHandler : ICompositionRequestHandler<OrderRequest, OrderReadModel>
{
    private readonly IComposer _composer;
    private readonly HttpClient _httpClient;

    public OrderCompositionRequestHandler(IComposer composer, IHttpClientFactory factory)
    {
        _composer = composer;
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5102");
    }

    public async Task<OrderReadModel> HandleAsync(OrderRequest request, CancellationToken cancellationToken = default)
    {
        var readModel = await _httpClient.GetFromJsonAsync<OrderReadModel>($"order/{request.OrderId}", cancellationToken);

        if (readModel is null)
        {
            throw new InvalidOperationException();
        }
        
        readModel.UserDetails.UserId = Guid.NewGuid(); // taken from identity
        await _composer.ComposeAsync(readModel, cancellationToken: cancellationToken);

        return readModel;
    }
}