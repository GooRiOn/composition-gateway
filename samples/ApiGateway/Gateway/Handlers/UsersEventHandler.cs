using ApiGateway.Gateway.ReadModels;
using CompositionGateway.Events;
using CompositionGateway.Handlers;

namespace ApiGateway.Gateway.Handlers;

public sealed class UsersEventHandler : ICompositionEventHandler<OrderReadModel>
{
    private readonly HttpClient _httpClient;

    public UsersEventHandler(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5101");
    }
    
    public async Task HandleAsync(CompositionRequested<OrderReadModel> @event, CancellationToken cancellationToken = default)
    {
        var userDetails = await _httpClient
            .GetFromJsonAsync<UserReadModel>($"/users/{@event.ReadModel.UserDetails.UserId}", cancellationToken);

        if (userDetails is null)
        {
            throw new InvalidOperationException();
        }
        
        @event.ReadModel.UserDetails = userDetails;
    }
}