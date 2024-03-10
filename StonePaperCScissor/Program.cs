
using StonePaperCScissor.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseRouting();
app.MapHub<MainGameHub>("/index");

app.Run();

