namespace CompositionGateway.Handlers;

public interface ICompositionRequestHandler<in TRequest, TReadModel> where TRequest : class where TReadModel : class
{
    Task<TReadModel> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}