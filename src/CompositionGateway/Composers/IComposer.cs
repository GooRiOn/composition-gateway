namespace CompositionGateway.Composers;

public interface IComposer
{
    Task ComposeAsync<TReadModel>(TReadModel readModel, CancellationToken cancellationToken = default) 
        where TReadModel : class;
}