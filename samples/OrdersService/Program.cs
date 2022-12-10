var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/order/{orderId}", (Guid orderId) => new
{
    Id = orderId,
    UserId = Guid.NewGuid(),
    Status = new Random().Next() % 2 is 0 ? "PENDING" : "COMPLETED",
    Products = new List<Guid>()
    {
        Guid.NewGuid(),
        Guid.NewGuid(),
        Guid.NewGuid(),
        Guid.NewGuid(),
        Guid.NewGuid(),
    }.Select(x => new {Id = x})
});

app.Run();