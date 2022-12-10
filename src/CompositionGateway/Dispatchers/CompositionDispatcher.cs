using CompositionGateway.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace CompositionGateway.Dispatchers;

internal sealed class CompositionDispatcher : ICompositionDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CompositionDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task<TReadModel> DispatchAsync<TRequest, TReadModel>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : class where TReadModel : class
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICompositionRequestHandler<TRequest, TReadModel>>();

        var result = await handler.HandleAsync(request).ConfigureAwait(false);
        return result;
    }
}