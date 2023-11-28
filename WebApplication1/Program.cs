using WebWeatherApi.Extensions;
using WebWeatherApi.Framework_and_drivers.Mapper;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDBContext(builder.Configuration.GetConnectionString("Dockerized"));
builder.Services.InjectServices();
builder.Services.AddCorsAny();
builder.Services.AddAutoMapper(typeof(WeatherMappingProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
