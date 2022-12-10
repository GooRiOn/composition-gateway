using System.Reflection;
using CompositionGateway.Composers;
using CompositionGateway.Dispatchers;
using CompositionGateway.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CompositionGateway;

public static class Extensions
{
    public static IServiceCollection AddCompositionGateway(this IServiceCollection services, Assembly[] assemblies = default)
    {
        assemblies ??= AppDomain.CurrentDomain.GetAssemblies();

        services.Scan(x => x.FromAssemblies(assemblies)
            .AddClasses(c => c
                .AssignableTo(typeof(ICompositionEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.Scan(x => x.FromAssemblies(assemblies)
            .AddClasses(c => c
                .AssignableTo(typeof(ICompositionRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddSingleton<ICompositionDispatcher, CompositionDispatcher>();
        services.AddSingleton<IComposer, Composer>();

        return services;
    }

    public static RouteHandlerBuilder ComposeGet<TRequest, TReadModel>(this IEndpointRouteBuilder endpoints, string pattern) 
        where TRequest : class where TReadModel : class 
        => endpoints.Compose<TRequest, TReadModel>(pattern, HttpMethod.Get.Method);
    
    public static RouteHandlerBuilder ComposePost<TRequest, TReadModel>(this IEndpointRouteBuilder endpoints, string pattern) 
        where TRequest : class where TReadModel : class 
        => endpoints.Compose<TRequest, TReadModel>(pattern, HttpMethod.Post.Method);
    
    private static RouteHandlerBuilder Compose<TRequest, TReadModel>(this IEndpointRouteBuilder endpoints, string pattern, string method) 
        where TRequest : class where TReadModel : class 
        => endpoints.MapMethods(pattern, new[] {method} ,async (TRequest request, [FromServices] ICompositionDispatcher dispatcher, CancellationToken cancellationToken) 
            => await dispatcher.DispatchAsync<TRequest, TReadModel>(request, cancellationToken));
}