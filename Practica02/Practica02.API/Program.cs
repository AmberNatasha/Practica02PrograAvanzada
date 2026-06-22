var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbConnectionFactory and repositories/services for DI´{+¿
builder.Services.AddSingleton<Practica02.API.Data.IDbConnectionFactory, Practica02.API.Data.DbConnectionFactory>();
builder.Services.AddScoped<Practica02.API.Repositories.IClienteRepository, Practica02.API.Repositories.ClienteRepository>();
builder.Services.AddScoped<Practica02.API.Repositories.IMascotaRepository, Practica02.API.Repositories.MascotaRepository>();
builder.Services.AddScoped<Practica02.API.Services.IClienteService, Practica02.API.Services.ClienteService>();
builder.Services.AddScoped<Practica02.API.Services.IMascotaService, Practica02.API.Services.MascotaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
