using ApiGateway.Gateway.ReadModels;
using ApiGateway.Gateway.Requests;
using CompositionGateway;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddHttpClient()
    .AddCompositionGateway();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.ComposeGet<OrderRequest, OrderReadModel>("/orders/{orderId}");

app.Run();