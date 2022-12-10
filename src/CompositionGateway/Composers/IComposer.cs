namespace CompositionGateway.Composers;

public interface IComposer
{
    Task ComposeAsync<TReadModel>(TReadModel readModel, IDictionary<string, string> baggage = default,
        CancellationToken cancellationToken = default) where TReadModel : class;
}