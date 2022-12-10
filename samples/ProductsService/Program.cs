using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/products", ([FromQuery] Guid[] productIds) => 
    productIds.Select((x, i) => new
    {
        Id = x,
        Name = $"Product {i}",
        Price = new Random().Next(100)
    }));

app.Run();