using ApiGateway.Gateway.ReadModels;
using CompositionGateway.Events;
using CompositionGateway.Handlers;

namespace ApiGateway.Gateway.Handlers;

public sealed class UsersEventHandler : ICompositionEventHandler<OrderReadModel>
{
    public Task HandleAsync(CompositionRequested<OrderReadModel> @event, CancellationToken cancellationToken = default)
    {
        @event.ReadModel.UserDetails = new UserReadModel
        {
            FullName = "Joe Doe",
            Email = "jdoe@gmail.com",
            ShippingAddress = "MailStreet 13, WDC"
        };

        return Task.CompletedTask;
    }
}