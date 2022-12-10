using CompositionGateway.Events;

namespace CompositionGateway.Handlers;

public interface ICompositionEventHandler<TReadModel> where TReadModel : class
{
    Task HandleAsync(CompositionRequested<TReadModel> @event, CancellationToken cancellationToken = default);
}