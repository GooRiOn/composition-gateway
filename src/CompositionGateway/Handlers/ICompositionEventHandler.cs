using CompositionGateway.Events;

namespace CompositionGateway.Handlers;

public interface ICompositionEventHandler<TReadModel> where TReadModel : class
{
    bool HandleWhen(CompositionRequested<TReadModel> @event) => true;
    Task HandleAsync(CompositionRequested<TReadModel> @event, CancellationToken cancellationToken = default);
}