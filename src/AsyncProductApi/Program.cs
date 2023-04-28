using AsyncProductApi;
using AsyncProductApi.Core;
using AsyncProductApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi();
builder.Services.AddCore();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseApi();
app.UseInfrastructure();

app.Run();
