using ApiGateway.Gateway.ReadModels;
using ApiGateway.Gateway.Requests;
using CompositionGateway;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCompositionGateway();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.ComposePost<OrderRequest, OrderReadModel>("/orders");

app.Run();