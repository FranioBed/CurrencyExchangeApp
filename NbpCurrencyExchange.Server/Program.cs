using Microsoft.Extensions.Configuration;
using NbpCurrencyExchange.Server.Data;
using NbpCurrencyExchange.Server.Services;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using NbpCurrencyExchange.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CurrencyExchangeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICurrencyExchangeRateService, CurrencyExchangeRateService>();
builder.Services.AddScoped<ICurrencyExchangeRepository, CurrencyExchangeRateRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    CurrencyExchangeDbContext dbContext = scope.ServiceProvider.GetRequiredService<CurrencyExchangeDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
