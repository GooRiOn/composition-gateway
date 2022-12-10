using CompositionGateway.Events;
using CompositionGateway.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace CompositionGateway.Composers;

internal sealed class Composer : IComposer
{
    private readonly IServiceProvider _serviceProvider;

    public Composer(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;
    
    public async Task ComposeAsync<TReadModel>(TReadModel readModel, CancellationToken cancellationToken = default)
        where TReadModel : class
    {
        using var scope = _serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<ICompositionEventHandler<TReadModel>>();

        var @event = new CompositionRequested<TReadModel>(readModel);
        var tasks = handlers.Select(x => x.HandleAsync(@event, cancellationToken));

        await Task.WhenAll(tasks);
    }
}