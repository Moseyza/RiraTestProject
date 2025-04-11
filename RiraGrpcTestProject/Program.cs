


using Microsoft.EntityFrameworkCore;
using RiragRPCTestProject.Data;
using RiragRPCTestProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TestInMemoryDb"));

var app = builder.Build();

app.MapGrpcService<PersonServiceImpl>();


// Configure the HTTP request pipeline.
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
