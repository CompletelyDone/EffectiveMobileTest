using Delivery.Application;
using Delivery.Core;
using Delivery.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
