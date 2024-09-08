using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Events;
using Stock.API.Consumers;
using Stock.API.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(conf =>
{
	conf.AddConsumer<OrderCreatedEventConsumer>();
	conf.AddConsumer<PaymentFailedEventConsumer>();
	conf.UsingRabbitMq((context, _configure) =>
	{
		_configure.Host(builder.Configuration["RabbitMQUrl"]);
		_configure.ReceiveEndpoint(RabbitMQSettings.Stock_OrderCreatedEventQueue,e=> e.ConfigureConsumer<OrderCreatedEventConsumer>(context));
		_configure.ReceiveEndpoint(RabbitMQSettings.Stock_PaymentFailedEventQueue, e => e.ConfigureConsumer<PaymentFailedEventConsumer>(context));
	});
});

builder.Services.AddDbContext<SagaStockDBContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MssqlConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
