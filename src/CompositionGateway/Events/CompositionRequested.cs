namespace CompositionGateway.Events;

public record CompositionRequested<TReadModel>(TReadModel ReadModel, IDictionary<string, string> Baggage) where TReadModel : class;