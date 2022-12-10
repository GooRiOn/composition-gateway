namespace CompositionGateway.Dispatchers;

internal interface ICompositionDispatcher
{
    Task<TReadModel> DispatchAsync<TRequest, TReadModel>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : class where TReadModel : class;
}