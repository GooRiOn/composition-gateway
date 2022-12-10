var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/users/{userId}", (Guid userId) => new
{
    Id = userId,
    FullName = "Joe Doe",
    Email = "jdoe@gmail.com"
});

app.Run();