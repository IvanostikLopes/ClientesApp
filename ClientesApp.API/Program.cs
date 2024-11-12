using ClientesApp.API.Extensions;
using ClientesApp.Domain.Services;
using ClientesApp.Infra.Data.SqlServer;
using ClientesApp.Infra.Data.SqlServer.Extensions;
using ClientesApp.Domain.Extensions;
using ClientesApp.Application.Extensions;
using ClientesApp.API.Middlewares;
using ClientesApp.Infra.Data.MongoDB.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Coloca o nome do controle na URL em caixa baixa
builder.Services.AddRouting(map => { map.LowercaseUrls = true; });
builder.Services.AddSweggerConfig();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddApplicationServices();
builder.Services.AddMongoDB(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseMiddleware<NotFoundExceptionMiddleware>();
app.UseSweggerConfig();
app.UseAuthorization();
app.MapControllers();
app.Run();
