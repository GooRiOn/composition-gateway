namespace CompositionGateway.Events;

public record CompositionRequested<TReadModel>(TReadModel ReadModel) where TReadModel : class;