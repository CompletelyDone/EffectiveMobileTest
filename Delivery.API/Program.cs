using Delivery.API.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("config.json");

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
