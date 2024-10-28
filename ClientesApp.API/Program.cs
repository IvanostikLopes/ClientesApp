using ClientesApp.API.Extensions;
using ClientesApp.Infra.Data.SqlServer;
using ClientesApp.Infra.Data.SqlServer.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Coloca o nome do controle na URL em caixa baixa
builder.Services.AddRouting(map => { map.LowercaseUrls = true; });
builder.Services.AddSweggerConfig();
builder.Services.AddEntityFramework(builder.Configuration);

var app = builder.Build();


app.UseSweggerConfig();
app.UseAuthorization();
app.MapControllers();
app.Run();
