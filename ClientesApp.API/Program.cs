using ClientesApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Coloca o nome do controle na URL em caixa baixa
builder.Services.AddRouting(map => { map.LowercaseUrls = true; });
builder.Services.AddSweggerConfig();

var app = builder.Build();


app.UseSweggerConfig();
app.UseAuthorization();
app.MapControllers();
app.Run();
