using sqrs_concepts.Domain.Orders;
using sqrs_concepts.Infrastructure.Redis;
using sqrs_concepts.Infrastructure.Streams;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var redisConnection = ConnectionMultiplexer.Connect("redis");
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
builder.Services.AddScoped<IEventWriter, RedisEventWriter>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

